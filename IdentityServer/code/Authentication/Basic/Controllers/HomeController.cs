using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.userClaims = HttpContext.User.Claims.ToList();

            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.custom")]
        public IActionResult SecretPolicy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            // 创建用户Claims
            var zhusirClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"zhusir"),
                new Claim(ClaimTypes.Email,"zhusir@zz.com"),
                new Claim(ClaimTypes.DateOfBirth,"2000-05-08"),
                new Claim("zhusir said","you are good"),
            };
            // 创建用户Claims
            var zhusirLicenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,"zhusir@zz.com"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("zhusir drivingLicense","A+"),
                new Claim(ClaimTypes.Name,"zhusirDirveLicense"),

            };
            // 创建用户身份
            var zhusirIdentity = new ClaimsIdentity(zhusirClaims, "zhusir Identity");
            var zhusirLicenseIdentity = new ClaimsIdentity(zhusirLicenseClaims, "Government");

            //设置用户主体
            var userPrincipal = new ClaimsPrincipal(new[] { zhusirIdentity, zhusirLicenseIdentity });

            // 在客户端生成用户主体的
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
        public IActionResult ResetCookie()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
