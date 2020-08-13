using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        public async Task<IActionResult> Open()
        {
            var cookieJar = new CookieJar(); // from db
            var requirement = new OperationAuthorizationRequirement()
            {
                Name = CookieJarOperations.Open
            };
            await _authorizationService.AuthorizeAsync(User, cookieJar, CookieJarAuthOperations.Open);
            return View();
        }
    }

    public class CookieJarAuthorizationHandler 
        : AuthorizationHandler<OperationAuthorizationRequirement, CookieJar>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement,
            CookieJar cookieJar)
        {
            if (requirement.Name == CookieJarOperations.Look)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                if (requirement.Name == CookieJarOperations.ComeNear)
                {
                    if (context.User.HasClaim("Friend","good"))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }

    public static class CookieJarAuthOperations
    {
        public static OperationAuthorizationRequirement Open = new OperationAuthorizationRequirement
        {
            Name = CookieJarOperations.Open
        };
    }

    public static class CookieJarOperations
    {
        public static string Open = "Open";
        public static string TakeCookie = "TakeCookie";
        public static string ComeNear = "ComeNear";
        public static string Look = "Look";
    }

    public class CookieJar
    {
        public string Name { get; set; }
    }
}
