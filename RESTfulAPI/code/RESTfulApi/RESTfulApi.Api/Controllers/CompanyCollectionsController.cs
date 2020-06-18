using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulApi.Api.Entities;
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection([FromBody] IEnumerable<CompanyAddDto> companyCollection)
        {

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var item in companyEntities)
            {
                _companyRepositroy.AddCompany(item);
            }

            await _companyRepositroy.SaveAsync();


            return Ok();

            //var returnDto = _mapper.Map<CompanyDto>(entity);

            //return CreatedAtRoute(nameof(GetCompany), new { companyId = returnDto.Id }, returnDto);
        }
    }
}
