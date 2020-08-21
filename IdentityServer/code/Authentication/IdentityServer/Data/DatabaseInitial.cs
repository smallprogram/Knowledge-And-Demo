using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class DatabaseInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly PersistedGrantDbContext _persistedGrantDbContext;
        private readonly ConfigurationDbContext _configurationDbContext;

        public DatabaseInitial(
            UserManager<IdentityUser> userManager,
            AppDbContext appDbContext,
            PersistedGrantDbContext persistedGrantDbContext,
            ConfigurationDbContext configurationDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _persistedGrantDbContext = persistedGrantDbContext;
            _configurationDbContext = configurationDbContext;
        }
        public async Task DbInit()
        {
            _appDbContext.Database.Migrate();
            if (!_appDbContext.Users.Any())
            {
                var user = new IdentityUser("zhusir");
                await _userManager.CreateAsync(user, "zhusir");
                IEnumerable<Claim> claims = new List<Claim>
                    {
                        new Claim("role", "admin"),
                        new Claim("role.apione", "apioneadmin"),
                        new Claim("scope.claim", "apionecliam"),
                    };
                await _userManager.AddClaimsAsync(user, claims);
            }

            _persistedGrantDbContext.Database.Migrate();
            _configurationDbContext.Database.Migrate();

            if (!_configurationDbContext.Clients.Any())
            {
                foreach (var client in Configuration.GetClients())
                {
                    _configurationDbContext.Clients.Add(client.ToEntity());
                }
                _configurationDbContext.SaveChanges();
            }

            if (!_configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in Configuration.GetIdentityResources())
                {
                    _configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }
                _configurationDbContext.SaveChanges();
            }

            if (!_configurationDbContext.ApiResources.Any())
            {
                foreach (var resource in Configuration.GetApis())
                {
                    _configurationDbContext.ApiResources.Add(resource.ToEntity());
                }
                _configurationDbContext.SaveChanges();
            }

            if (!_configurationDbContext.ApiScopes.Any())
            {
                foreach (var resource in Configuration.GetApiScopes())
                {
                    _configurationDbContext.ApiScopes.Add(resource.ToEntity());
                }
                _configurationDbContext.SaveChanges();
            }
        }
    }
}
