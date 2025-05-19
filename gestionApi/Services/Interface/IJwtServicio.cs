using gestionApi.Models;

namespace gestionApi.Services.Interface;

public interface IJwtServicio
{
    string CrearToken(Usuario usuario);
}