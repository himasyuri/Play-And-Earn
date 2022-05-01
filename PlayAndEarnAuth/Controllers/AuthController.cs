using Microsoft.AspNetCore.Mvc;
using PlayAndEarnAuth.Models.Dto;
using PlayAndEarnAuth.Services;

namespace PlayAndEarnAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IJwtGeneratorService _jwtGeneratorService;

        public AuthController(IUserAuthService userAuthService, IJwtGeneratorService jwtGeneratorService)
        {
            _userAuthService = userAuthService;
            _jwtGeneratorService = jwtGeneratorService;

        }

        [Route("Registration")]
        [HttpPost]
        public async Task<ActionResult<string>> Registration([FromBody] UserRegistration request)
        {
            var result = await _userAuthService.Register(request);
            var token = await _jwtGeneratorService.CreateAccessToken(result);
            return Ok(token);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin request)
        {
            var result = await _userAuthService.LoginAsync(request);
            if (result != null)
            {
                var token = await _jwtGeneratorService.CreateAccessToken(result);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
