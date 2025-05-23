﻿using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class ConductorRepositorio : IConductorRepositorio
{
    private readonly IDbConnection _bd;
    public ConductorRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }
    public async Task<IEnumerable<Conductor>> GetConductores()
    {
        string mysql = "SELECT *FROM Conductor";
        var resultado = await _bd.QueryAsync<Conductor>(mysql);
        return resultado;
    }

    public async Task BorrarConductor(int id)
    {
        string mysql = "DELETE FROM Conductor WHERE IdConductor=@IdConductor";
        await _bd.ExecuteAsync(mysql, new { IdConductor = id });
    }

    public async Task<Conductor?> GetConductor(int id)
    {
        string mysql = "SELECT * FROM Conductor WHERE IdConductor = @IdConductor";
        var resultado = await _bd.QueryFirstOrDefaultAsync<Conductor>(mysql, new { IdConductor = id });
        return resultado;
    }

    public async Task<Conductor> AgregarConductor(Conductor conductor)
    {
        string verificarConductor = "SELECT COUNT(*) FROM Conductor WHERE DniConductor = @DniConductor AND EstadoConductor = conductor.EstadoConductor";
        int existeConductor = await _bd.ExecuteScalarAsync<int>(verificarConductor, new { conductor.DniConductor });

        if (existeConductor > 0)
        {
            throw new InvalidOperationException("El conductor con ese DNI ya está registrado.");
        }

        string mysql = @"INSERT INTO Conductor (NombreConductor, ApellidoConductor, TelefonoConductor,DniConducctor, EstadoConductor)
                        VALUES (@NombreConductor, @ApellidoConductor, @TelefonoConductor, @DniConducctor, @EstadoConductor);
                        SELECT LAST_INSERT_ID();";

        conductor.IdConductor = await _bd.ExecuteScalarAsync<int>(mysql, conductor);
        return conductor;
    }

    public async Task<Conductor> ActualizarConductor(Conductor conductor)
    {
        var mysql = "UPDATE Conductor SET NombreConductor=@NombreConductor, ApellidoConductor=@ApellidoConductor, TelefonoConductor=@TelefonoConductor,DniConductor=@DniConducctor, EstadoConductor=@EstadoConductor " +
                            "WHERE IdConductor=@IdConductor";
        await _bd.ExecuteAsync(mysql, conductor);
        return conductor;
    }
}