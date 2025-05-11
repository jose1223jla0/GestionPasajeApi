using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Vehiculo
{
    [Key]
    public int IdVehiculo { get; set; }
    [Required]
    public string? PlacaVehiculo { get; set; }
    [Required]
    public string? MarcaVehiculo { get; set; }
    [Required]
    public string? ModeloVehiculo { get; set; }
    [Required]
    public string? ColorVehiculo { get; set; }
    [Required]
    public int CapacidadVehiculo { get; set; }
    [Required]
    public bool EstadoVehiculo { get; set; }
    [Required]
    [ForeignKey(nameof(IdConductor))]
    public int IdConductor { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionVehiculo { get; set; }
    /*==================================================================
                        relaciones
    ====================================================================*/
    public Conductor? Conductor { get; set; }
    public List<Horario>? Horarios { get; set; } = new List<Horario>();

}