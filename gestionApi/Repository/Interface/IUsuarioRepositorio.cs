using gestionApi.Models;
namespace gestionApi.Repository.Interface;
public interface IUsuarioRepositorio
{
    Task<Usuario?> GetUsuario(int id);
    Task<IEnumerable<Usuario>> GetUsuarios();
    Task<Usuario> AgregarUsuario(Usuario usuario);
    Task<Usuario> EditarUsuario(Usuario usuario);
}
