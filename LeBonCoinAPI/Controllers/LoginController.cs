using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LeBonCoinAPI.Models;
using LeBonCoinAPI.Models.EntityFramework;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<Particulier> appUsers = new List<Particulier>
        {
        new Particulier {Email = "test@gmail.com", Civilite = "H", Nom = "Orchant", Prenom = "Yves", DateNaissance = new DateTime(1989,06,02) },
        };

        public LoginController(IConfiguration config)
        {
            _config = config;
        }



        //LoginParticulier
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginParticulier([FromBody] Particulier login)
        {
            IActionResult response = Unauthorized();
            Particulier user = AuthenticateParticulier(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        //AuthenticateUser
        private Particulier AuthenticateParticulier(Particulier user)
        {
            return appUsers.SingleOrDefault(x => x.Email.ToUpper() == user.Email.ToUpper() && x.HashMotDePasse == user.HashMotDePasse);
        }


        //GenerateJwtToken
        private string GenerateJwtToken(Particulier userInfo)
        {
            var securityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
