using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.IdentityServer
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
                new IdentityResource
                {
                    Name ="role.scope",
                    UserClaims =
                    {
                        "role"
                    }
                }
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
                    Scopes ={ "ApiOne.read", "ApiOne.all" }
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

                    RedirectUris ={"https://localhost:17004/signin-oidc" },
                    PostLogoutRedirectUris = {"https://localhost:17004/Home/Index" },


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

                    RequirePkce = true,

                    AllowOfflineAccess = true, // 启用refresh_token
                    //AlwaysIncludeUserClaimsInIdToken = true, //UserClaim包含到idToken中
                },

                // Blazor Authorization code Client
                new Client
                {
                    ClientId = "client_id_blazor",
                    //ClientSecrets = {new Secret("client_secret_blazor".ToSha256())},
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris ={"https://localhost:25004/authentication/login-callback" },
                    PostLogoutRedirectUris = {"https://localhost:25004/" },
                    AllowedCorsOrigins = {"https://localhost:25004" },
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne.read",
                        //"ApiTwo.read",
                        "role.scope",

                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    
                    RequireConsent = false, //是否需要用户确认授权

                    RequirePkce = true,

                    AllowOfflineAccess = true, // 启用refresh_token
                    //AlwaysIncludeUserClaimsInIdToken = true, //UserClaim包含到idToken中

                     AccessTokenLifetime = 10,
                },
                new Client
                {
                    ClientId = "client_id_js",
                    //ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    RequirePkce = true,
                    RequireClientSecret = false,


                    RedirectUris ={"https://localhost:17005/Home/SignIn" },
                    PostLogoutRedirectUris = {"https://localhost:17005/Home/Index" },
                    AllowedCorsOrigins = { "https://localhost:17005" }, //允许客户端跨域请求
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "ApiOne.read",
                        "role.scope",
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false, //是否需要用户确认授权


                    AccessTokenLifetime = 1,

                    //yu
                    //AlwaysIncludeUserClaimsInIdToken = true, //UserClaim包含到idToken中
                },

            };
        }
    }
}
