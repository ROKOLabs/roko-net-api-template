namespace Roko.Template.Tests.Integration
{
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
 
    internal class IntegrationTestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        public const string AuthenticationType = "IntegrationTest";
        public const string AuthenticationScheme = "IntegrationTest";
        public const string IntegrationTestUserName = "IntegrationTest User";
        
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Claim[] claims = new[] {
                new Claim(ClaimTypes.Name, IntegrationTestUserName),
                new Claim(ClaimTypes.NameIdentifier, nameof(IntegrationTestUserName)),
                new Claim("a-custom-claim", "squirrel 🐿️"),
            };

            AuthenticateResult authenticationResult =
                AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(
                            new ClaimsIdentity(claims, AuthenticationType)),
                        AuthenticationScheme));

            return Task.FromResult(authenticationResult);
        }
    }
}