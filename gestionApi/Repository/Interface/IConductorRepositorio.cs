using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IConductorRepositorio
{
    Task<IEnumerable<Conductor>> GetConductores();
    Task BorrarConductor(int id);//como void
    Task<Conductor?> GetConductor(int id);
    Task<Conductor> AgregarConductor(Conductor conductor);
    Task<Conductor> ActualizarConductor(Conductor conductor);
}