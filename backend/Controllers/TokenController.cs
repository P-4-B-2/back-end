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
        private const string AiApiKeyConfigKey = "AI:ApiKey";

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateAiToken([FromBody] AiAuthRequest request)
        {
            var aiApiKey = _configuration[AiApiKeyConfigKey];
            if (request.ApiKey != aiApiKey)
            {
                return Unauthorized(new { message = "Invalid API key." });
            }

            string aiUid = "ai-service";

            var additionalClaims = new Dictionary<string, object>
            {
                { "role", "AI" }
            };

            string token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(aiUid, additionalClaims);

            return Ok(new { token });
        }
    }

    public class AiAuthRequest
    {
        public string ApiKey { get; set; }
    }
}
