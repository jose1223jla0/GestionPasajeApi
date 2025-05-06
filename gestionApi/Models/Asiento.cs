using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Asiento
{
    [Key]
    public int IdAsiento { get; set; }
    [Required]
    public bool EstadoAsiento { get; set; }
    [ForeignKey(nameof(IdHorario))]
    public int IdHorario { get; set; }
    /*===============================================
                //relaciones
    ================================================*/
    public Horario? Horario { get; set; }
}