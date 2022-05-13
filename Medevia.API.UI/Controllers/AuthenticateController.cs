using Medevia.API.UI.Applications.DTOs;
using Medevia.Core.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medevia.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Fields
        private readonly SecurityOption _options;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion
        #region Constructor
        public AuthenticateController(ILogger<AuthenticateController> logger, UserManager<IdentityUser> userManager, IOptions<SecurityOption> options)
        {
            _userManager = userManager;
            _options = options.Value;
            _logger = logger;
        }
        #endregion

        #region Public methods
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = BadRequest();
            var user = new IdentityUser(dtoUser.Login)
            {
                Email = dtoUser.Login,
                UserName = dtoUser.Name
            };
            var success = await _userManager.CreateAsync(user,dtoUser.Password);

            if (success.Succeeded)
            {
                dtoUser.Token = GenerateJwtToken(user); 
                result = Ok(dtoUser);
            }
            return result;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = BadRequest();
            try
            {
                

                var user = await _userManager.FindByEmailAsync(dtoUser.Login);
                if (user != null)
                {
                    var verif = await _userManager.CheckPasswordAsync(user, dtoUser.Password);
                    if (verif)
                    {
                        result = Ok(new AuthenticateUserDto()
                        {
                            Login = user.Email,
                            Name = user.UserName,
                            Token = GenerateJwtToken(user),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Login", ex, dtoUser);
                result = Problem("cannot login");
            }
           
            return result;
        }


        #endregion
        #region Internal Methods
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        #endregion
    }
}
