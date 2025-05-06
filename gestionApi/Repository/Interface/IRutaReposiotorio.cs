using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IRutaReposiotorio
{
    Task EliminarRuta(int id);
    Task<IEnumerable<Ruta>> GetRutas();
    Task<Ruta?> GetRuta(int id);
    Task<Ruta> AgregarRuta(Ruta ruta);
    Task<Ruta> ActualizarRuta(Ruta ruta);
}