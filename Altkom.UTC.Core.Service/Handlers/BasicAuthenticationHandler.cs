using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsersService usersService;

        public BasicAuthenticationHandler(
            IUsersService usersService,
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

            this.usersService = usersService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            var username = credentials[0];
            var password = credentials[1];


            User user = usersService.Authenticate(username, password);

            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            IIdentity identity = new ClaimsIdentity(Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // IIdentity identity = new GenericIdentity(user.Login);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
