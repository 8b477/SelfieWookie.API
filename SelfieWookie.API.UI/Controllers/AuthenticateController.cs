using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SelfieWookie.API.UI.Apllications.DTO;
using SelfieWookie.Core.Infrastructure.Configurations;

namespace SelfieWookie.API.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        #region DI
        private readonly ILogger<AuthenticateController>? _logger;
        private readonly SecurityOption? _options;
        private readonly UserManager<IdentityUser>? _userManager;

        public AuthenticateController(ILogger<AuthenticateController> logger, UserManager<IdentityUser> userManager, IOptions<SecurityOption> options)
        {
            _logger = logger;
            _options = options.Value;
            _userManager = userManager;
            _logger.LogDebug("tesst depers");
        }
        #endregion

        /// <summary>
        /// Enregistrement d'un nouveaux User, renvoie un Token si accepter.
        /// </summary>
        /// <param name="dtoUser"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = BadRequest();

            var user = new IdentityUser
            {
                Email = dtoUser.Login,
                UserName = dtoUser.Name
            };
            var success = await _userManager.CreateAsync(user, dtoUser.Password);

            if (success.Succeeded)
            {
                dtoUser.Token = GenerateJwtToken(user);
                result = Ok(dtoUser);
            }

            return result;
        }



        /// <summary>
        /// Permet la connection d'un User déjà existant en base de données
        /// </summary>
        /// <param name="dtoUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]AuthenticateUserDto dtoUser)
        {

            IActionResult result = BadRequest();

            try
            {


            var user = await _userManager.FindByEmailAsync(dtoUser.Login);

            var passIsValide = await _userManager.CheckPasswordAsync(user, dtoUser.Password);

            if (passIsValide)
            {
                if (user is not null)
                {
                        result = Ok(new AuthenticateUserDto()
                        {
                            Login = user.Email,
                            Name = user.UserName,
                            Token = GenerateJwtToken(user)
                        });      
                }
            }

            }
            catch (Exception ex)
            {

                _logger.LogError("Une erreur blabla..", ex, dtoUser);
            }
            return result;
        }


        /// <summary>
        /// A DEPLACER -> Genère un token sur des infos perso (User).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_options?.Key);
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
    }
}
