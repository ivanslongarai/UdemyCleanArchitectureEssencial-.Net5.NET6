using CleanArchMvc.API.DTOs;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    public class TokenController : Controller
    {
        private readonly IAuthenticate _authenticate;
        private readonly ILogger<TokenController> _logger;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, ILogger<TokenController> logger, IConfiguration configuration)
        {
            _authenticate = authenticate;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("api/v1/users/register")]
        // [ApiExplorerSettings(IgnoreApi = true)] //s In case is needed to hide this
        public async Task<ActionResult<UserTokenDTO>> Register([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation("Registering a new user, user {1}", loginDTO.Email);

            var result = await _authenticate.RegisterUserAsync(loginDTO.Email, loginDTO.Password);
            if (result == true)
            {
                _logger.LogInformation($"User {loginDTO.Email} registered successfully");
                return Ok(GenerateToken(loginDTO));
            }
            else
            {
                _logger.LogError("Eror registering a new user");
                ModelState.AddModelError("Messages", "Eror registering a new user.");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("api/v1/users/login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation("Calling Login, user {1}", loginDTO.Email);

            var result = await _authenticate.AuthenticateAsync(loginDTO.Email, loginDTO.Password);
            if (result == true)
            {
                _logger.LogInformation($"User {loginDTO.Email} login successfully");
                return Ok(GenerateToken(loginDTO));
            }
            else
            {
                _logger.LogError("Invalid Login attempt.");
                ModelState.AddModelError("Messages", "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserTokenDTO GenerateToken(LoginDTO loginDTO)
        {
            _logger.LogInformation($"Generating token for {loginDTO.Email}");

            try
            {
                var claims = new[]
                {
                    new Claim("email", loginDTO.Email),
                    new Claim("myValue", "whatever is needed"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.UtcNow.AddMinutes(30);

                JwtSecurityToken token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: expiration,
                        signingCredentials: credentials
                    );

                _logger.LogInformation($"Generated token for {loginDTO.Email} successfully");
                return new UserTokenDTO
                {
                    Email = loginDTO.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem at generation token for {loginDTO.Email}");
                return new UserTokenDTO
                {
                    Email = "Problem at generation token"
                };
            }
        }
    }
}
