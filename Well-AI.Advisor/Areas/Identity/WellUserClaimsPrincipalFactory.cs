using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity
{
    public class WellUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<WellIdentityUser, IdentityRole>
    {
        public WellUserClaimsPrincipalFactory(
            UserManager<WellIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(WellIdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("TenantId", user.TenantId ?? "[Click to edit profile]"));
            return identity;
        }
    }
}
