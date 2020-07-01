using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using RESTfulApi.Api.ActionConstraints;
using RESTfulApi.Api.DtoParameters;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Helpers;
using RESTfulApi.Api.Models;
using RESTfulApi.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepositroy _companyRepositroy;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public CompaniesController(ICompanyRepositroy companyRepositroy, IMapper mapper, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
        {
            _companyRepositroy = companyRepositroy ?? throw new ArgumentNullException(nameof(companyRepositroy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpHead]
        [HttpGet(Name = nameof(GetCompanies))]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameters parameters, [FromHeader(Name = "Accept")] string mediaType)  //ActionResult<IEnumerable<DtoCompany>
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!_propertyMappingService.ValidMappingExistsFor<CompanyDto, Company>(parameters.OrderBy))
            {
                return BadRequest();
            }
            if (!_propertyCheckerService.TypeHasProperites<CompanyDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var companies = await _companyRepositroy.GetCompaniesAsync(parameters);


            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            var shapeData = companyDtos.ShapeData(parameters.Fields);


            if (parsedMediaType.MediaType == "application/vnd.company.hateoas+json")
            {
                var paginationMetadata_hateoas = new
                {
                    totalCount = companies.TotalCount,
                    pageSize = companies.PageSize,
                    currentPage = companies.CurrentPage,
                    totalPage = companies.TotalPages,
                    //privousPageLink = companies.HasPrevious ? CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage) : null,
                    //nextPageLink = companies.HasNext ? CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage) : null
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata_hateoas, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping })
                    );

                var links = CreateLinksForCompany(parameters, companies.HasPrevious, companies.HasNext);

                var shapedCompaniesWithLinks = shapeData.Select(c =>
                {
                    var companyDict = c as IDictionary<string, object>;
                    var companyLinks = CreateLinksForCompany((Guid)companyDict["Id"], null);
                    companyDict.Add("links", companyLinks);
                    return companyDict;
                });

                var linkedCollectionResource = new
                {
                    value = shapedCompaniesWithLinks,
                    links = links
                };
                return Ok(linkedCollectionResource);
            }

            var paginationMetadata = new
            {
                totalCount = companies.TotalCount,
                pageSize = companies.PageSize,
                currentPage = companies.CurrentPage,
                totalPage = companies.TotalPages,
                privousPageLink = companies.HasPrevious ? CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage) : null,
                nextPageLink = companies.HasNext ? CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage) : null
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping })
                );

            return Ok(shapeData);

        }

        [Produces(
            "application/json", 
            "application/vnd.company.hateoas+json",
            "application/vnd.company.company.friendly+json",
            "application/vnd.company.company.friendly.hateoas+json",
            "application/vnd.company.company.full+json",
            "application/vnd.company.company.full.hateoas+json"
            )]
        [HttpGet("{companyId}", Name = nameof(GetCompany))]  // "api/Companies/{companyId}"
        public async Task<IActionResult> GetCompany(Guid companyId, string fields, [FromHeader(Name = "Accept")] string mediaType)
        {


            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperites<CompanyDto>(fields))
            {
                return BadRequest();
            }
            var company = await _companyRepositroy.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            //判断Accept结尾之后包含hateoas  application/vnd.company.hateoas+json =>  vnd.company.hateoas
            bool includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
            //创建hateoas的links
            IEnumerable<LinkDto> myLinks = new List<LinkDto>();
            if (includeLinks)
            {
                myLinks = CreateLinksForCompany(companyId, fields);
            }


            var primaryMediaType = includeLinks ? 
                parsedMediaType.SubTypeWithoutSuffix.Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8) : parsedMediaType.SubTypeWithoutSuffix;
            if (primaryMediaType == "vnd.company.company.full")
            {
                var full = _mapper.Map<CompanyFullDto>(company).shapeData(fields) as IDictionary<string, object>;
                if (includeLinks)
                {
                    full.Add("links", myLinks);
                }
                return Ok(full);
            }

            var friendly = _mapper.Map<CompanyDto>(company).shapeData(fields) as IDictionary<string, object>;
            if (includeLinks)
            {
                friendly.Add("links", myLinks);
            }
            return Ok(friendly);



            //if (parsedMediaType.MediaType == "application/vnd.company.hateoas+json")
            //{

            //    var links = CreateLinksForCompany(companyId, fields);

            //    var linkedDict = _mapper.Map<CompanyDto>(company).shapeData(fields) as IDictionary<string, object>;

            //    linkedDict.Add("links", links);

            //    return Ok(linkedDict);
            //}


            //return Ok(_mapper.Map<CompanyDto>(company).shapeData(fields));
        }

        [HttpPost(Name = nameof(CreateCompany))]
        [RequestHeaderMatchesMediaType("Content-Type","application/json","application/vnd.company.companyforcreation+json")]
        [Consumes("application/json", "application/vnd.company.companyforcreation+json")]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CompanyAddDto company)
        {
            // 使用了api controller就不需要这个判断了。
            //if (company == null)
            //{
            //    return BadRequest();
            //}


            var entity = _mapper.Map<Company>(company);
            _companyRepositroy.AddCompany(entity);
            await _companyRepositroy.SaveAsync();

            var returnDto = _mapper.Map<CompanyDto>(entity);


            var links = CreateLinksForCompany(returnDto.Id, null);

            var linkedDict = returnDto.shapeData(null) as IDictionary<string, object>;

            linkedDict.Add("links", links);

            return CreatedAtRoute(nameof(GetCompany), new { companyId = linkedDict["Id"] }, linkedDict);
        }

        [HttpPost(Name = nameof(CreateCompanyAddWithBankruptTime))]
        [RequestHeaderMatchesMediaType("Content-Type", "application/json", "application/vnd.company.companyforcreationwihtbankrupttime+json")]
        [Consumes("application/vnd.company.companyforcreationwihtbankrupttime+json")]
        public async Task<ActionResult<CompanyDto>> CreateCompanyAddWithBankruptTime([FromBody] CompanyAddWithBankruptTimeDto company)
        {
            // 使用了api controller就不需要这个判断了。
            //if (company == null)
            //{
            //    return BadRequest();
            //}


            var entity = _mapper.Map<Company>(company);
            _companyRepositroy.AddCompany(entity);
            await _companyRepositroy.SaveAsync();

            var returnDto = _mapper.Map<CompanyDto>(entity);


            var links = CreateLinksForCompany(returnDto.Id, null);

            var linkedDict = returnDto.shapeData(null) as IDictionary<string, object>;

            linkedDict.Add("links", links);

            return CreatedAtRoute(nameof(GetCompany), new { companyId = linkedDict["Id"] }, linkedDict);
        }

        [HttpDelete("{companyId}", Name = nameof(DeleteCompany))]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepositroy.GetCompanyAsync(companyId);

            if (companyEntity == null)
            {
                return NotFound();
            }

            await _companyRepositroy.GetEmployeesAsync(companyId, new EmployeeDtoParameters());

            _companyRepositroy.DeleteCompany(companyEntity);

            await _companyRepositroy.SaveAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCompanyOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

        #region helper
        private string CreateCompaniesResourceUri(CompanyDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(
                        nameof(GetCompanies),
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            companyName = parameters.CompanyName,
                            searchTerm = parameters.SearchTerm
                        });
                case ResourceUriType.NextPage:
                    return Url.Link(
                        nameof(GetCompanies),
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            companyName = parameters.CompanyName,
                            searchTerm = parameters.SearchTerm
                        });
                case ResourceUriType.CurrentPage:
                    return Url.Link(
                        nameof(GetCompanies),
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            companyName = parameters.CompanyName,
                            searchTerm = parameters.SearchTerm
                        });
                default:
                    return Url.Link(
                        nameof(GetCompanies),
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            companyName = parameters.CompanyName,
                            searchTerm = parameters.SearchTerm
                        });
            }
        }


        private IEnumerable<LinkDto> CreateLinksForCompany(Guid companyId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(
                    Url.Link(nameof(GetCompany), new { companyId }),
                    "self",
                    "GET")
                    );
            }
            else
            {
                links.Add(new LinkDto(
                    Url.Link(nameof(GetCompany), new { companyId, fields }),
                    "self",
                    "GET")
                    );
            }
            links.Add(new LinkDto(
                Url.Link(nameof(DeleteCompany), new { companyId }),
                "delete_company",
                "DELETE")
                );

            links.Add(new LinkDto(
                Url.Link(nameof(EmployeesController.CreateEmployeeForCompany), new { companyId }),
                "create_employee_for_company",
                "POST")
                );
            links.Add(new LinkDto(
                Url.Link(nameof(EmployeesController.GetEmployeesForCompany), new { companyId }),
                "employees",
                "GET")
                );

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForCompany(CompanyDtoParameters parameters, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(CreateCompaniesResourceUri(parameters, ResourceUriType.CurrentPage), "self", "GET"));

            if (hasPrevious)
            {
                links.Add(new LinkDto(CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage), "previous_page", "GET"));
            }
            if (hasNext)
            {
                links.Add(new LinkDto(CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage), "next_page", "GET"));
            }

            return links;
        }
        #endregion
    }
}
