using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IPasajeroRepositorio
{
    Task<Pasajero> BuscarPorDni(string dni);
    Task<Pasajero> AgregarPasajero(Pasajero pasajero);
    Task<IEnumerable<Pasajero>> GetPasajeros();
    Task<Pasajero> EditarPasajero(Pasajero pasajero);
}