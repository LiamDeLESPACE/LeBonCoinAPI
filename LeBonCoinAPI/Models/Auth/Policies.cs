using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace LeBonCoinAPI.Models.Auth
{
    public class Policies
    {
        public const string admin = nameof(Admin);
        public const string particulier = nameof(Particulier);
        public const string entreprise = nameof(Entreprise);
        public const string human = nameof(Particulier) + "," + nameof(Admin);
        public const string director = nameof(Entreprise) + "," + nameof(Admin);
        public const string all = nameof(Entreprise) + "," + nameof(Particulier) + "," + nameof(Admin);

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(admin).Build();
        }
        public static AuthorizationPolicy ParticulierPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(particulier).Build();
        }

        public static AuthorizationPolicy EntreprisePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(entreprise).Build();
        }

        public static AuthorizationPolicy HumanPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(particulier,admin).Build();
        }

        public static AuthorizationPolicy DirectorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(entreprise,admin).Build();
        }

        public static AuthorizationPolicy AllPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(entreprise,particulier,admin).Build();
        }

    }
}
