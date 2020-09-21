using IdentityServer4.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.IdentityServer.Pages.Auth
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        IIdentityServerInteractionService IdentityServerInteractionService { set; get; }
        [Inject]
        SignInManager<IdentityUser> SignInManager { set; get; }
        [Inject]
        NavigationManager NavigationManager { set; get; }


        public string logoutId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new Uri(NavigationManager.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("logoutId", out var value))
            {
                logoutId = value;
            }
            if (!String.IsNullOrWhiteSpace(logoutId))
            {
                await SignInManager.SignOutAsync();

                var logoutRequest = await IdentityServerInteractionService.GetLogoutContextAsync(logoutId);

                if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
                {
                    NavigationManager.NavigateTo("/");
                }
                NavigationManager.NavigateTo(logoutRequest.PostLogoutRedirectUri);
            }
        }

    }
}
