using System.ComponentModel.DataAnnotations;

namespace gestionApi.Models;

public class Conductor
{
    [Key]
    public int IdConductor { get; set; }
    [Required]
    public string? NombConductor { get; set; }
    [Required]
    public string? ApellConductor { get; set; }
    [Required]
    public string? TelfConductor { get; set; }
    [Required]
    public bool EstadoConductor { get; set; }
    /*=================================================================
                    //relaciones
    ===================================================================*/
    public List<Vehiculo>? Vehiculos { get; set; } = new List<Vehiculo>();
}