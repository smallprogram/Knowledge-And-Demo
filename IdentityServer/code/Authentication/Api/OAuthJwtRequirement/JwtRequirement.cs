using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Api.OAuthJwtRequirement
{
    public class JwtRequirement : IAuthorizationRequirement
    {

    }

    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;

        public JwtRequirementHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            JwtRequirement requirement)
        {
            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var accessToken = authHeader.ToString().Split(' ')[1];

                var serverResponse = await _httpClient
                    .GetAsync($"https://localhost:6021/oauth/tokenvalidate?access_token={accessToken}");

                if (serverResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
