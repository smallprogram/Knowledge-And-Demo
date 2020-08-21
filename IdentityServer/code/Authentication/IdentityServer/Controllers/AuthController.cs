using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Org.BouncyCastle.Ocsp;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IEmailService _emailService;

        public AuthController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IIdentityServerInteractionService identityServerInteractionService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // 登录业务

            var user = await _userManager.FindByNameAsync(vm.Username);

            if (user != null)
            {
                // sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return Redirect(vm.ReturnUrl);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
        [HttpPost]
        public async Task<IActionResult> Logout(LoginViewModel vm)
        {
            // 登录业务

            var user = await _userManager.FindByNameAsync(vm.Username);

            if (user != null)
            {
                // sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return Redirect(vm.ReturnUrl);
                }
            }
            return BadRequest();
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel {ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // 注册业务
            var user = new IdentityUser
            {
                UserName = vm.Username,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                // sign user here 登录账户
                // 生成Email Token

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var link = Url.Action(nameof(VerifyEmial), "Home", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());
                //await _emailService.SendAsync("181171302@qq.com", "email verify", link);
                //return RedirectToAction("EmailVerification");
                await _signInManager.SignInAsync(user, false);
                return Redirect(vm.ReturnUrl);
            }

            return BadRequest();
        }
        public async Task<IActionResult> VerifyEmial(string userId, string code)
        {
            // 验证EmailToken
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
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
