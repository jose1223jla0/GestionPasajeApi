using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;
namespace gestionApi.Repository;
public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IDbConnection _bd;
    public UsuarioRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task<Usuario> AgregarUsuario(Usuario usuario)
    {
        usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

        string mysql = "INSERT INTO Usuario (NombreUsuario, Contrasena, EstadoUsuario) " +
                       "VALUES (@NombreUsuario, @Contrasena, @EstadoUsuario)";
        usuario.IdUsuario = await _bd.ExecuteScalarAsync<int>(mysql, usuario);
        return usuario;
    }

    public async Task<Usuario?> GetUsuario(int id)
    {
        string mysql = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
        Usuario? resultado = await _bd.QueryFirstOrDefaultAsync<Usuario>(mysql, new { IdUsuario = id });
        return resultado;
    }

    public async Task<IEnumerable<Usuario>> GetUsuarios()
    {
        string mysql = "SELECT * FROM Usuario";
        IEnumerable<Usuario> resultado = await _bd.QueryAsync<Usuario>(mysql);
        return resultado;
    }

    public async Task<Usuario> EditarUsuario(Usuario usuario)
    {
        string mysql = "UPDATE Usuario SET NombreUsuario = @NombreUsuario, " +
                        "Contrasena = @Contrasena, EstadoUsuario = @EstadoUsuario WHERE IdUsuario = @IdUsuario";

        await _bd.ExecuteAsync(mysql, usuario);
        return usuario;
    }
}
