using System.Data;
using Dapper;
using gestionApi.Models;
using gestionApi.Repository.Interface;
using MySql.Data.MySqlClient;

namespace gestionApi.Repository;

public class HorarioRepositorio : IHorarioRepositorio
{
    private readonly IDbConnection _bd;

    public HorarioRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task<IEnumerable<Horario>> GetHorarios()
    {
        string mysql = "SELECT h.IdHorario,h.FechaSalida,h.HoraSalida, h.Precio,h.EstadoHorario ," +
                       " v.IdVehiculo,v.PlacaVehiculo,v.MarcaVehiculo, v.ModeloVehiculo, v.CapacidadVehiculo ," +
                       "v.EstadoVehiculo   " +
                       "FROM HORARIO h INNER JOIN VEHICULO v ON h.IdVehiculo = v.IdVehiculo";
        IEnumerable<Horario> resultado = await _bd.QueryAsync<Horario, Vehiculo, Horario>(mysql, MapHorarioVehiculo, splitOn: "IdVehiculo");
        return resultado;
    }


    public async Task<Horario?> GetHorario(int id)
    {
        string mysql = "SELECT * FROM Horario WHERE IdHorario=@IdHorario";
        Horario? resultado = await _bd.QueryFirstOrDefaultAsync<Horario>(mysql, new { IdHorario = id });
        return resultado;
    }

    public async Task<Horario> AgregarHorario(Horario horario)
    {
        
        string mysql = "INSERT INTO Horario (FechaSalida, HoraSalida, Precio,EstadoHorario, IdVehiculo,IdRuta) " +
                       "VALUES (@FechaSalida, @HoraSalida, @Precio,EstadoHorario, @IdVehiculo,@IdRuta)";
        horario.IdHorario = await _bd.ExecuteScalarAsync<int>(mysql, horario);
        return horario;
    }

    public async Task<Horario> ActualizarHorario(Horario horario)
    {
        string mysql =
            "UPDATE Horario SET FechaSalida=@FechaSalida, HoraSalida=@HoraSalida, Precio=@Precio, EstadoHorario=@EstadoHorario, IdVehiculo=@IdVehiculo, IdRuta=@IdRuta " +
            "WHERE IdHorario=@IdHorario";
        await _bd.ExecuteAsync(mysql, horario);
        return horario;
    }

    public async Task BorrarHorario(int id)
    {
        string mysql = "DELETE FROM Horario WHERE IdHorario=@IdHorario";
        await _bd.ExecuteAsync(mysql, new { IdHorario = id });
    }
    
    private Horario MapHorarioVehiculo(Horario horario, Vehiculo vehiculo)
    {
        horario.Vehiculo = vehiculo;
        return horario;
    }

}