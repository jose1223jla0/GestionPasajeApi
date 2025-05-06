using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class VehiculoRepositorio : IVehiculoRepositorio
{
    private readonly IDbConnection _bd;

    public VehiculoRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task<IEnumerable<Vehiculo>> GetVehiculos()
    {
        string mysql = "SELECT * FROM vehiculo";
        var resultado = await _bd.QueryAsync<Vehiculo>(mysql);
        return resultado;
    }

    public async Task<Vehiculo?> GetVehiculo(int id)
    {
        string mysql = "SELECT * FROM vehiculo WHERE IdVehiculo=@IdVehiculo";
        Vehiculo? resultado = await _bd.QueryFirstOrDefaultAsync<Vehiculo>(mysql, new { IdVehiculo = id });
        return resultado;
    }

    public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
    {
        string mysql =
            "INSERT INTO vehiculo (PlacaVehiculo, MarcaVehiculo, ModeloVehiculo, CapVehiculo, EstadoVehiculo, IdConductor) " +
            "VALUES (@PlacaVehiculo, @MarcaVehiculo, @ModeloVehiculo,@CapVehiculo, @EstadoVehiculo, @IdConductor)";
        vehiculo.IdVehiculo = await _bd.ExecuteScalarAsync<int>(mysql, vehiculo);
        return vehiculo;
    }

    public async Task<Vehiculo> ActualizarVehiculo(Vehiculo vehiculo)
    {
        string mysql =
            "UPDATE vehiculo SET PlacaVehiculo=@PlacaVehiculo, MarcaVehiculo=@MarcaVehiculo, ModeloVehiculo=@ModeloVehiculo, CapVehiculo=@CapVehiculo, EstadoVehiculo=@EstadoVehiculo, IdConductor=@IdConductor " +
            "WHERE IdVehiculo=@IdVehiculo";
        await _bd.ExecuteAsync(mysql, vehiculo);
        return vehiculo;
    }
}