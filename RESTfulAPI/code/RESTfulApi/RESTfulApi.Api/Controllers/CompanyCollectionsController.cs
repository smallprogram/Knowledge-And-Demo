using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Helpers;
using RESTfulApi.Api.Models;
using RESTfulApi.Api.Services;

namespace RESTfulApi.Api.Controllers
{
    [Route("api/companycollections")]
    [ApiController]
    public class CompanyCollectionsController : ControllerBase
    {
        private readonly ICompanyRepositroy _companyRepositroy;
        private readonly IMapper _mapper;

        public CompanyCollectionsController(ICompanyRepositroy companyRepositroy, IMapper mapper)
        {
            _companyRepositroy = companyRepositroy ?? throw new ArgumentNullException(nameof(companyRepositroy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})",Name = nameof(GetCompanyCollection))]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType =typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var entities = await _companyRepositroy.GetCompaniesAsync(ids);
            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }

            var dtoToReturn = _mapper.Map<IEnumerable<CompanyDto>>(entities);

            return Ok(dtoToReturn);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection([FromBody] IEnumerable<CompanyAddDto> companyCollection)
        {

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var item in companyEntities)
            {
                _companyRepositroy.AddCompany(item);
            }

            await _companyRepositroy.SaveAsync();


            //return Ok();

            var returnDto = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var idsString = string.Join(",", returnDto.Select(x => x.Id));

            return CreatedAtRoute(nameof(GetCompanyCollection), new { ids = idsString }, returnDto);
        }
    }
}
