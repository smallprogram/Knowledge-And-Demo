using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOne.Controllers
{
    public class SecretController: Controller
    {
        [Route("api/secret")]
        [Authorize]
        public string Index()
        {
            return "secret message from apione";
        }
    }
}
