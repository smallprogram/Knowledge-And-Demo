using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Api.Controllers
{
    public class SecretController : Controller
    {
        [Route("api/secret")]
        [Authorize]
        public string Index()
        {
            var claims = User.Claims.ToList();
            return "secret message from apione";
        }
    }
}
