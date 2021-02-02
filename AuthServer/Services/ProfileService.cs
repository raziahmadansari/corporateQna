using AuthServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> UserManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("name", user.Name),
                new Claim("designation", user.Designation),
                new Claim("team", user.Team),
                new Claim("category", user.Category),
                new Claim("location", user.Location)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}
