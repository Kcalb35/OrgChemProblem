using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrgChemProblem.Controllers;

namespace OrgChemProblem.Models
{
    public class AuthenticateService :IAuthenticate
    {
        private ProblemDbcontext _Context;
        private TokenManagement _Token;

        public AuthenticateService(ProblemDbcontext context,IOptions<TokenManagement> token)
        {
            _Context = context;
            _Token = token.Value;
        }
        public async Task<(bool, string)> IsAuthenticated(LoginReq req)
        {
            var user = await _Context.Managers.FirstOrDefaultAsync(x => x.UserName == req.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password,user.HashPassword))
            {
                return (false, string.Empty);
            }
            else
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,
                        $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        $"{new DateTimeOffset(DateTime.Now.AddHours(12)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, req.Username)
                };

                var skey = _Token.Secret;
                var domain = _Token.Issuer;
                var audience = _Token.Audience;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(skey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: domain,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: creds
                );
                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
                return (true, jwttoken);
            }
        }
    }
}