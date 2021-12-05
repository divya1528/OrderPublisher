using ApplicationCore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repository
{
    public class JWTAuthManager : IJWTAuthManager
    {
        IConfiguration _configuration;

        public JWTAuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response<string> GenerateJWT(User user)
        {
            Response<string> response = new Response<string>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim("roles", user.Role),
                 new Claim("Date", DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_configuration["JwtAuth:Issuer"],
              _configuration["JwtAuth:Issuer"],
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            response.Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token
            response.code = 200;
            response.message = "Token generated";
            return response;
        }

        public Response<List<T>> getUserList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
