
using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IAuthRepositorio
{
    Task<Usuario> Login(string usuarioNombre, string contrasena);
    Task<bool> ExisteUsuario(string usuarioNombre);
}
