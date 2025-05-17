using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class RutaRepositorio : IRutaReposiotorio
{
    private readonly IDbConnection _bd;

    public RutaRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task EliminarRuta(int id)
    {
        string mysql = "DELETE FROM Ruta WHERE IdRuta=@IdRuta";
        await _bd.ExecuteAsync(mysql, new { IdRuta = id });
    }

    public async Task<IEnumerable<Ruta>> GetRutas()
    {
        string mysql = "SELECT * FROM Ruta";
        IEnumerable<Ruta> resultado = await _bd.QueryAsync<Ruta>(mysql);
        return resultado;
    }

    public async Task<Ruta?> GetRuta(int id)
    {
        string mysql = "SELECT * FROM Ruta WHERE IdRuta=@IdRuta";
        Ruta? resultado = await _bd.QueryFirstOrDefaultAsync<Ruta>(mysql, new { IdRuta = id });
        return resultado;
    }

    public async Task<Ruta> AgregarRuta(Ruta ruta)
    {
        string verificarRuta = "SELECT COUNT(*) FROM Ruta WHERE OrigenRuta = @OrigenRuta AND DestinoRuta = @DestinoRuta";
        int existeRuta = await _bd.ExecuteScalarAsync<int>(verificarRuta, new { ruta.OrigenRuta, ruta.DestinoRuta });
        if (existeRuta > 0)
        {
            throw new InvalidOperationException("La ruta ya está registrada.");
        }

        string mysql = "INSERT INTO Ruta (OrigenRuta, DestinoRuta, DuracionRuta, EstadoRuta) " +
                       "VALUES (@OrigenRuta, @DestinoRuta, @DuracionRuta, @EstadoRuta)";
        ruta.IdRuta = await _bd.ExecuteScalarAsync<int>(mysql, ruta);
        return ruta;
    }

    public async Task<Ruta> ActualizarRuta(Ruta ruta)
    {
        string mysql =
            "UPDATE Ruta SET OrigenRuta=@OrigenRuta, DestinoRuta=@DestinoRuta, DuracionRuta=@DuracionRuta, EstadoRuta=@EstadoRuta " +
            "WHERE IdRuta=@IdRuta";
        await _bd.ExecuteAsync(mysql, ruta);
        return ruta;
    }
}