using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class PasajeroRepositorio : IPasajeroRepositorio
{
    private readonly IDbConnection _db;

    public PasajeroRepositorio(IConfiguration configuration)
    {
        _db = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task<Pasajero> BuscarPorDni(string dni)
    {
        string mysql = "SELECT * FROM Pasajero WHERE DniPasajero=@DniPasajero";
        return await _db.QueryFirstAsync(mysql, new { DniPasajero = dni });

    }

    public async Task InsertarPasajero(Pasajero pasajero)
    {
        string mysql = "INSERT INTO Pasajero (NombrePasajero, ApellidoPasajero, DniPasajero, TelefPasajero) " +
                       "VALUES (@NombrePasajero, @ApellidoPasajero, @DniPasajero, @TelefPasajero)";
        
        pasajero.IdPasajero = await _db.ExecuteScalarAsync<int>(mysql, pasajero);
    }

    public async Task<IEnumerable<Pasajero>> GetPasajeros()
    {
        var mysql = "SELECT * FROM Pasajero";
        return await _db.QueryAsync<Pasajero>(mysql);
    }
}