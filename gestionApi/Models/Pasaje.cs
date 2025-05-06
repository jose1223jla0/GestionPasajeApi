using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Pasaje
{
    [Key]
    public int IdPasaje { get; set; }
    [Required]
    public DateTime FechaventaPasaje { get; set; }
    [Required]
    public bool EstadoPasaje { get; set; }
    [Required]
    [ForeignKey(nameof(IdPasajero))]
    public int IdPasajero { get; set; }
    [Required]
    [ForeignKey(nameof(IdHorario))]   
    public int IdHorario { get; set; }
    /*===============================================
    //relaciones
    =================================================*/
    public Pasajero? Pasajero { get; set; }
    public Pago? Pago { get; set; }
    public Horario? Horario { get; set; }
}