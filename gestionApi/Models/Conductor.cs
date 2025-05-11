using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Conductor
{
    [Key]
    public int IdConductor { get; set; }
    [Required]
    public string? NombreConductor { get; set; }
    [Required]
    public string? ApellidoConductor { get; set; }
    [Required]
    public string? TelefonoConductor { get; set; }
    [Required]
    public bool EstadoConductor { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionConductor { get; set; }
    /*=================================================================
                    //relaciones
    ===================================================================*/
    public List<Vehiculo>? Vehiculos { get; set; } = new List<Vehiculo>();
}