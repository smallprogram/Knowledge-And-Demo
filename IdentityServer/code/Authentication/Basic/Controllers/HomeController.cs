using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Basic.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
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
        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View();
        }
        [SecurityLevel(10)]
        public IActionResult SecretHighLevel()
        {
            return View();
        }

        public async Task<IActionResult> DoStuff()
        {
            var builder = new AuthorizationPolicyBuilder("policy1");
            var customPolicy = builder.RequireClaim("claimname").Build();

            var authResult = await _authorizationService.AuthorizeAsync(HttpContext.User, customPolicy);
            if (authResult.Succeeded)
            {

            }
            return View("Index");
        }
        public async Task<IActionResult> DoStuff2([FromServices] IAuthorizationService authorizationService)
        {
            var builder = new AuthorizationPolicyBuilder("policy1");
            var customPolicy = builder.RequireClaim("claimname").Build();

            var authResult = await authorizationService.AuthorizeAsync(HttpContext.User, customPolicy);
            if (authResult.Succeeded)
            {
                return View("Index");
            }
            return View("Index");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Authenticate()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromForm] string key)
        {
            if (key != "test")
            {
                return RedirectToAction(nameof(AccessDenied));
            }
            // 创建用户Claims
            var zhusirClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"zhusir"),
                new Claim(ClaimTypes.Email,"zhusir@zz.com"),
                new Claim(ClaimTypes.DateOfBirth,"2000-05-08"),
                new Claim(DynamicPilicies.SecurityLevel,"7"),
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
            //return Redirect(ReturnUrl);
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
