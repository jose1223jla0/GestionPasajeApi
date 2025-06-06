﻿using System.Data;
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

    public async Task<Pasajero> AgregarPasajero(Pasajero pasajero)
    {
        string verificarPasajero = "SELECT COUNT(*) FROM Pasajero WHERE DniPasajero = @DniPasajero";
        int existePasajero = await _db.ExecuteScalarAsync<int>(verificarPasajero, new { pasajero.DniPasajero });
        if (existePasajero > 0)
        {
            throw new InvalidOperationException("El pasajero ya está registrado.");
        }

        string mysql = "INSERT INTO Pasajero (NombrePasajero, ApellidoPasajero, DniPasajero, TelefonoPasajero) " +
                       "VALUES (@NombrePasajero, @ApellidoPasajero, @DniPasajero, @TelefonoPasajero)";

        pasajero.IdPasajero = await _db.ExecuteScalarAsync<int>(mysql, pasajero);
        return pasajero;
    }

    public async Task<Pasajero> EditarPasajero(Pasajero pasajero)
    {
        string mysql = "UPDATE Pasajero SET NombrePasajero=@NombrePasajero, ApellidoPasajero=@ApellidoPasajero, " +
                       "DniPasajero=@DniPasajero, TelefonoPasajero=@TelefonoPasajero WHERE IdPasajero=@IdPasajero";
        await _db.ExecuteAsync(mysql, pasajero);
        return pasajero;
    }

    public async Task<IEnumerable<Pasajero>> GetPasajeros()
    {
        var mysql = "SELECT * FROM Pasajero";
        return await _db.QueryAsync<Pasajero>(mysql);
    }
}