using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IPasajeroRepositorio
{
    Task<Pasajero> BuscarPorDni(string dni);
    Task InsertarPasajero(Pasajero pasajero);
    Task<IEnumerable<Pasajero>> GetPasajeros();
}