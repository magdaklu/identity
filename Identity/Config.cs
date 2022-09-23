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
                    RedirectUris = { "https://localhost:3000/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:3000/sidnout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:3000/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "TutorAPI.read", "TutorAPI.write" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
                new Client
                {
                    // unique ID for this client
                    ClientId = "tutor", 
                    // human-friendly name displayed in IS
                    ClientName = "Tutor", 
                    // URL of client
                    ClientUri = "http://localhost:3000", 
                    // how client will interact with our identity server (Implicit is basic flow for web apps)
                    AllowedGrantTypes = GrantTypes.Implicit, 
                    // don't require client to send secret to token endpoint
                    RequireClientSecret = false,
                    RedirectUris =
                    {             
                        // can redirect here after login                     
                        "http://localhost:3000/signin-oidc",
                    },
                    // can redirect here after logout
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" }, 
                    // builds CORS policy for javascript clients
                    AllowedCorsOrigins = { "http://localhost:3000" }, 
                    // what resources this client can access
                    AllowedScopes = { "openid", "profile", "TutorAPI.read", "TutorAPI.write" }, 
                    // client is allowed to receive tokens via browser
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
