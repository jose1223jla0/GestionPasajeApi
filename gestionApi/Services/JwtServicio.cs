using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gestionApi.Models;
using gestionApi.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace gestionApi.Services;

public class JwtServicio : IJwtServicio
{
    private readonly SymmetricSecurityKey _clave;

    public JwtServicio(IConfiguration configuration)
    {
        _clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
    }

    public string CrearToken(Usuario usuario)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, usuario.NombreUsuario!),
            new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
        };
        SigningCredentials credenciales = new SigningCredentials(_clave, SecurityAlgorithms.HmacSha256);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credenciales,
            Issuer = "http://localhost:4200",  
            Audience = "http://localhost:4200" 
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}