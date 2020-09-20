using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Blazor.IdentityServer.ViewModels;
using Blazor.IdentityServer.Middleware;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.IdentityServer.Pages.Auth
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { set; get; }
        [Inject]
        UserManager<IdentityUser> _userManager { set; get; }
        [Inject]
        SignInManager<IdentityUser> _signInManager { set; get; }

        protected LoginModel<IdentityUser> vm { get; set; } = new LoginModel<IdentityUser>();

        protected override void OnInitialized()
        {
            var query = new Uri(NavigationManager.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("ReturnUrl", out var value))
            {
                vm.ReturnUrl = value;
            }
        }


        protected async Task Submit()
        {
            var query = new Uri(NavigationManager.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("ReturnUrl", out var value))
            {
                vm.ReturnUrl = value;
            }
            vm.Error = null;
            var usr = await _userManager.FindByNameAsync(vm.UserName);
            if (usr == null)
            {
                vm.Error = "Login failed. Check your username and password.";
                return;
            }


            if (await _signInManager.CanSignInAsync(usr))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(usr, vm.Password, true);
                if (result.Succeeded)
                {
                    Guid key = BlazorCookieLoginMiddleware<IdentityUser>.AnnounceLogin(vm);
                    NavigationManager.NavigateTo($"/login?redirecturl={vm.ReturnUrl}&key={key}", true);
                }
                else
                {
                    vm.Error = "Login failed. Check your username and password.";
                }
            }
            else
            {
                vm.Error = "Your account is blocked";
            }
        }
    }
}
