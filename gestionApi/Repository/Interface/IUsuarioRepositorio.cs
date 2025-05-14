using gestionApi.Models;
namespace gestionApi.Repository.Interface;
public interface IUsuarioRepositorio
{
    Task<Usuario?> GetUsuario(int id);
}
