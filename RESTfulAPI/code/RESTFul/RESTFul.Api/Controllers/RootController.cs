using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFul.Models.Dto;

namespace RESTFul.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link(nameof(GetRoot), new { }),
                "self",
                "GET"
                ));

            links.Add(
                new LinkDto(Url.Link(nameof(CompaniesController.GetCompanies), new { }),
                "companies",
                "GET"
                ));
            links.Add(
                new LinkDto(Url.Link(nameof(CompaniesController.CreateCompany), new { }),
                "self",
                "POST"
                ));


            return Ok(links);
        }
    }
}
