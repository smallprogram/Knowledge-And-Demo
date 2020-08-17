using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        public string Index()
        {
            return "secret message form Api";
        }
    }
}
