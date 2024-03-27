using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace LeBonCoinAPI.Models.Auth
{
    public class Policies
    {
        public const string admin = nameof(Admin);
        public const string particulier = nameof(Particulier);
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(admin).Build();
        }
        public static AuthorizationPolicy ParticulierPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(particulier).Build();
        }
    }
}
