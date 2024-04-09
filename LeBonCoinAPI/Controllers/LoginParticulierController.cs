using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LeBonCoinAPI.Models;
using LeBonCoinAPI.Models.EntityFramework;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginParticulierController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContext _data;
        private List<Particulier> appUsers;

        public LoginParticulierController(IConfiguration config, DataContext datacontext)
        {
            _config = config;
            _data = datacontext;
            appUsers = _data.Particuliers.ToList<Particulier>();
        }


        //LoginParticulier
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Login([FromBody] Particulier login)
        {

            IActionResult response = Unauthorized();
            Particulier user = AuthenticateParticulier((Particulier)login);
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
            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(user.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            return appUsers.SingleOrDefault(x => x.Email.ToUpper() == user.Email.ToUpper() && x.HashMotDePasse.ToUpper() == sb.ToString().ToUpper());
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
            new Claim("prenom", userInfo.Prenom.ToString()),
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
