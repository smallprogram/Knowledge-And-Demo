using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulApi.Api.Services;

namespace RESTfulApi.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepositroy _companyRepositroy;

        public CompaniesController(ICompanyRepositroy companyRepositroy)
        {
            _companyRepositroy = companyRepositroy ?? throw new ArgumentNullException(nameof(companyRepositroy));
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()  //ActionResult<IEnumerable<DtoCompany>
        {
            var companies = await _companyRepositroy.GetCompaniesAsync();

            return Ok(companies);
        }

        [HttpGet("{companyId}")]  // "api/Companies/{companyId}"
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _companyRepositroy.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            
            return Ok(company);
        }
    }
}
