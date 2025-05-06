using System.ComponentModel.DataAnnotations;

namespace gestionApi.Models;

public class Ruta
{
    [Key]
    public int IdRuta { get; set; }
    [Required]
    public string? OrigenRuta { get; set; }
    [Required]
    public string? DestinoRuta { get; set; }
    [Required]
    public TimeSpan DuracionRuta { get; set; }
    [Required]
    public bool EstadoRuta { get; set; }
    /*=============================================================
                    //relaciones
    ==============================================================*/
    public List<Horario>? Horarios { get; set; } = new List<Horario>();
}