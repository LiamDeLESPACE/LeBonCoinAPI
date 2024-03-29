using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LeBonCoinAPI.Models;
using LeBonCoinAPI.Models.EntityFramework;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginEntrepriseController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContext _data;
        private List<Entreprise> appUsers;

        public LoginEntrepriseController(IConfiguration config, DataContext datacontext)
        {
            _config = config;
            _data = datacontext;
            appUsers = _data.Entreprises.ToList<Entreprise>();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Entreprise login)
        {

            IActionResult response = Unauthorized();
            Entreprise user = AuthenticateEntreprise(login);
            if (user != null)
            {
                var tokenString = GenerateJwtTokenEntreprise(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        private Entreprise AuthenticateEntreprise(Entreprise user)
        {
            return appUsers.SingleOrDefault(x => x.Siret.ToUpper() == user.Siret.ToUpper() && x.HashMotDePasse == user.HashMotDePasse);
        }

        private string GenerateJwtTokenEntreprise(Entreprise userInfo)
        {
            var securityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Siret),
            new Claim("role",userInfo.GetType().Name),
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
