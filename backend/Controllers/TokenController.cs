using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateAiToken([FromBody] AiAuthRequest request)
        {
            if (!Request.Headers.TryGetValue("Handshake-Token", out var providedToken))
            {
                return Unauthorized(new { message = "Missing handshake token." });
            }

            var expectedToken = _configuration["AI__ApiKey"];
            //Console.WriteLine(expectedToken);
            
            if (string.IsNullOrEmpty(expectedToken))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Server handshake token is not configured." });
            }

            if (providedToken != expectedToken)
            {
                return Unauthorized(new { message = "Invalid handshake token. provided token: " + providedToken + ", expected token: " + expectedToken});
            }

            string token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync("neKcRwZ7cANUkV6Hi9yneIjYoCs2");

            return Ok(new { token });
        }
    }

    public class AiAuthRequest
    {
        public string? ApiKey { get; set; }
    }
}
