using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(name: "ApiOne.read",   displayName: "Read your data."),
                new ApiScope(name: "ApiOne.write",   displayName: "write your data."),
                new ApiScope(name: "ApiOne.all",   displayName: "management apione"),
                new ApiScope(name: "ApiTwo.read",   displayName: "Read your data."),
            };
        }
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                // Aud
                new ApiResource(name:"https://localhost:7002/api/secret")
                {
                    Scopes ={ "ApiOne.read" }
                },
                new ApiResource(name:"ApiTwo")
                {
                  Scopes = { "ApiTwo.read" }
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // ClientCredentials Client
                new Client
                {
                    ClientName = "ClientCredentials Client",
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256())},

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = {"ApiOne.read" },

                },

                // Authorization code Client
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris ={"https://localhost:7004/signin-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne.read",
                        "ApiTwo.read"
                    },

                }
            };
        }
    }
}
