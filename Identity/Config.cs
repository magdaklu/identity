using IdentityServer4.Models;

namespace Identity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("TutorAPI.read"), new ApiScope("TutorAPI.write") };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            { 
                new ApiResource("TutorAPI")
                {
                    Scopes = new List<string> { "TutorAPI.read", "TutorAPI.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> { "role" }
                }
            };
        public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = { "TutorAPI.read", "TutorAPI.write" }
                },
                // code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7225/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7225/sidnout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7225/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "TutorAPI.read", "TutorAPI.write" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                }
            };
    }
}
