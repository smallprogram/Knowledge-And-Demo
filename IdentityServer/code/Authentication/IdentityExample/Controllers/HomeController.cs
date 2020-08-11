using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Org.BouncyCastle.Ocsp;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // 登录业务

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                // sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // 注册业务
            var user = new IdentityUser
            {
                UserName = username,
                Email = "",
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // sign user here 登录账户
                // 生成Email Token

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(VerifyEmial), "Home", new { userId = user.Id, code },Request.Scheme,Request.Host.ToString());
                await _emailService.SendAsync("181171302@qq.com", "email verify", link);
                return RedirectToAction("EmailVerification");
            }

            return RedirectToAction("Index");
        }
        public  IActionResult VerifyEmial(string userId, string code)
        {
            // 验证EmailToken


            return View();
        }

        public IActionResult EmailVerification()
        {
            return View();
        }



        public async Task<IActionResult> LogOut()
        {
            // 注销业务
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
