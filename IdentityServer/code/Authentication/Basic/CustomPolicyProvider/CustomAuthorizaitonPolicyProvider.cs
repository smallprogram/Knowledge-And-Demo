using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Basic.CustomPolicyProvider
{
    public class SecurityLevelAttribute : AuthorizeAttribute
    {
        public SecurityLevelAttribute(int level)
        {
            Policy = $"{DynamicPilicies.SecurityLevel}.{level}";
        }
    }

    public static class DynamicPilicies
    {
        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }
        public const string SecurityLevel = "SecurityLevel";
        public const string Rank = "Rank";
    }
    public static class DynamicAtuhorizationPilicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            var parts = policyName.Split('.');
            var type = parts.First();
            var value = parts.Last();

            switch (type)
            {
                case DynamicPilicies.Rank:
                    return new AuthorizationPolicyBuilder()
                        .RequireClaim("Rank",value)
                        .Build();
                case DynamicPilicies.SecurityLevel:
                    return new AuthorizationPolicyBuilder()
                        .AddRequirements(new SecurityLevelRequirement(Convert.ToInt32(value)))
                        .Build();
                default:
                    return null;
            }
        }
    }
    public class SecurityLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; }
        public SecurityLevelRequirement(int level)
        {
            Level = level;
        }
    }

    public class SecurityLevelHandler : AuthorizationHandler<SecurityLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SecurityLevelRequirement requirement)
        {
            var calimValue =
                Convert.ToInt32(
                context.User.Claims.FirstOrDefault(x => x.Type == DynamicPilicies.SecurityLevel)?.Value ?? "0"
                );
            if (requirement.Level <= calimValue)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class CustomAuthorizaitonPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizaitonPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            foreach (var customPolicy in DynamicPilicies.Get())
            {
                if (policyName.StartsWith(customPolicy))
                {
                    var policy = DynamicAtuhorizationPilicyFactory.Create(policyName);
                    return Task.FromResult(policy);
                }

            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
