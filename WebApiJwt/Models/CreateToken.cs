using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiJwt.Models
{
    public class CreateToken
    {
        public string TokenCreate()
        {
            var bytes = Encoding.UTF8.GetBytes("uzun-ve-guclu-bir-anahtar-olduburasi");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);  
        }
        public string AdminToken()
        {
            var bytes = Encoding.UTF8.GetBytes("uzun-ve-guclu-bir-anahtar-olduburasi");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Visitor")
            };
            JwtSecurityToken token = new JwtSecurityToken(issuer:"https://localhost", audience:"https://localhost", notBefore: DateTime.Now,expires:DateTime.Now.AddMinutes(10), signingCredentials:credentials, claims:claims);
            JwtSecurityTokenHandler handler= new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
