using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TrackFinances.Api.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(Guid userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:SecretKey")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(issuer: _configuration.GetValue<string>("Authentication:Issuer"),
                                         audience: _configuration.GetValue<string>("Authentication:Audience"),
                                         claims: claims,
                                         expires: DateTime.Now.AddDays(5),
                                         signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
