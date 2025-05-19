using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class AuthRepositorio : IAuthRepositorio
{
    private readonly IDbConnection _bd;
    public AuthRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }
    public async Task<bool> ExisteUsuario(string usuarioNombre)
    { 
        string existeUsuario = "SELECT COUNT(*) FROM Usuario WHERE NombreUsuario=@NombreUsuario";
        var resultado = await _bd.QueryFirstOrDefaultAsync<int>(existeUsuario, new { NombreUsuario = usuarioNombre });
        return resultado > 0;
    }
    
    public async Task<Usuario> Login(string usuarioNombre, string contrasena)
    {
        string consultaLogin = "SELECT * FROM Usuario WHERE NombreUsuario=@NombreUsuario";
        var usuario = await _bd.QueryFirstOrDefaultAsync<Usuario>(consultaLogin, new { NombreUsuario = usuarioNombre });
        
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
        {
            throw new InvalidOperationException("Usuario o contraseña incorrectos");
        }
        
        return usuario;
    }
}
