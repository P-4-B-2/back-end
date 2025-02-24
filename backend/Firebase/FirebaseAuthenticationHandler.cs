﻿using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace backend.Firebase
{
    public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        // private const string AiApiKey = "your-static-api-key";
        private readonly IConfiguration _configuration;

        public FirebaseAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IConfiguration configuration)
            : base(options, logger, encoder)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header missing");
            }

            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string AiApiKey = _configuration["AI__ApiKey"];

            if (token == AiApiKey)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "AI"),
                    new Claim(ClaimTypes.Role, "AI")
                };

                var identity = new ClaimsIdentity(claims, nameof(FirebaseAuthenticationHandler));
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                DateTime expired = DateTimeOffset.FromUnixTimeSeconds(decodedToken.ExpirationTimeSeconds).DateTime;
                if (expired < DateTime.UtcNow)
                {
                    return AuthenticateResult.Fail("Bearer Token has expired!");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid),
                    new Claim(ClaimTypes.Email, decodedToken.Claims["email"]?.ToString() ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, nameof(FirebaseAuthenticationHandler));
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (FirebaseAuthException ex)
            {
                return AuthenticateResult.Fail($"Firebase auth failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Firebase auth failed: {ex.Message}");
            }
        }
    }
}
