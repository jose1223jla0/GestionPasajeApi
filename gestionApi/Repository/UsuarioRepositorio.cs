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


    public async Task<Usuario?> GetUsuario(int id)
    {
        string mysql = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
        Usuario? resultado = await _bd.QueryFirstOrDefaultAsync<Usuario>(mysql, new { IdUsuario = id });
        return resultado;
    }
}
