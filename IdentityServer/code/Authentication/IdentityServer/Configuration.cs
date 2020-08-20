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
        #region IdentityToken的组成部分
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource
                //{
                //    Name ="role.scope",
                //    UserClaims =
                //    {
                //        "role"
                //    }
                //}
            };
        }
        #endregion

        #region AccessToken的组成部分
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            // Scope
            return new List<ApiScope>
            {
                new ApiScope(name: "ApiOne.read",   displayName: "Read your data.",userClaims:new string[]{"scope.claim" }),
                new ApiScope(name: "ApiOne.write",   displayName: "write your data."),
                new ApiScope(name: "ApiOne.all",   displayName: "management apione"),
                new ApiScope(name: "ApiTwo.read",   displayName: "Read your data."),
            };
        }
        public static IEnumerable<ApiResource> GetApis()
        {
            // Aud
            return new List<ApiResource>
            {
                // Aud
                new ApiResource(name:"ApiOne",displayName:"ApiOneResource",userClaims:new string[]{ "role.apione" })
                {
                    Scopes ={ "ApiOne.read" }
                },
                new ApiResource(name:"ApiTwo")
                {
                  Scopes = { "ApiTwo.read" }
                },
            };
        }
        #endregion

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
                        "ApiTwo.read",
                        //"role.scope",

                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    RequireConsent = false, //是否需要用户确认授权

                    AllowOfflineAccess = true, // 启用refresh_token

                    //AlwaysIncludeUserClaimsInIdToken = true, //UserClaim包含到idToken中


                }
            };
        }
    }
}
