using gestionApi.Models;

namespace gestionApi.Repository.Interface;

public interface IHorarioRepositorio
{
    Task<IEnumerable<Horario>> GetHorarios();
    Task<Horario?> GetHorario(int id);
    Task<Horario> AgregarHorario(Horario horario);
    Task<Horario> ActualizarHorario(Horario horario);
    Task BorrarHorario(int id);
}