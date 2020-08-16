using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            return View();
        }

        public IActionResult ResetCookie()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
