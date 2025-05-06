using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IVehiculoRepositorio
{
    Task<IEnumerable<Vehiculo>> GetVehiculos();
    Task<Vehiculo?> GetVehiculo(int id);
    Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo);
    Task<Vehiculo> ActualizarVehiculo(Vehiculo vehiculo);
}