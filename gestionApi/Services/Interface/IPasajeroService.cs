using gestionApi.Models.ModelViewDTO;

namespace gestionApi.Services.Interface;

public interface IPasajeroService
{
    Task<PasajeroDto?> BuscarPorDni(string dni);
}