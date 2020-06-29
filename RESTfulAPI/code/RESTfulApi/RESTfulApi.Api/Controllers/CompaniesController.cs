using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RESTfulApi.Api.DtoParameters;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Helpers;
using RESTfulApi.Api.Models;
using RESTfulApi.Api.Services;

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
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameters parameters)  //ActionResult<IEnumerable<DtoCompany>
        {

            if (!_propertyMappingService.ValidMappingExistsFor<CompanyDto, Company>(parameters.OrderBy))
            {
                return BadRequest();
            }
            if (!_propertyCheckerService.TypeHasProperites<CompanyDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var companies = await _companyRepositroy.GetCompaniesAsync(parameters);

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

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos.ShapeData(parameters.Fields));
        }

        [HttpGet("{companyId}", Name = nameof(GetCompany))]  // "api/Companies/{companyId}"
        public async Task<IActionResult> GetCompany(Guid companyId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperites<CompanyDto>(fields))
            {
                return BadRequest();
            }
            var company = await _companyRepositroy.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var companyDto = _mapper.Map<CompanyDto>(company);

            var links = CreateLinksForCompany(companyId, fields);

            var linkedDict = _mapper.Map<CompanyDto>(company).shapeData(fields) as IDictionary<string, object>;

            linkedDict.Add("links", links);

            return Ok(linkedDict);
        }

        [HttpPost]
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


        //[HttpDelete("{companyId}")]
        //public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId)
        //{
        //    if (!await _companyRepositroy.CompanyExistsAsync(companyId))
        //    {
        //        return NotFound();
        //    }
        //    var company = await _companyRepositroy.GetCompanyAsync(companyId);

        //    _companyRepositroy.DeleteCompany(company);

        //    await _companyRepositroy.SaveAsync();

        //    return NoContent();
        //}


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
                Url.Link(nameof(DeleteCompany), new { companyId}),
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
        #endregion
    }
}
