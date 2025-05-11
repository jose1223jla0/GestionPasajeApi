using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Horario
{
    [Key]
    public int IdHorario { get; set; }
    [Required]
    public DateTime FechaSalida { get; set; }
    [Required]
    public TimeSpan HoraSalida { get; set; }
    [Required]
    public decimal Precio { get; set; }
    [Required]
    public bool EstadoHorario { get; set; }
    [Required]
    [ForeignKey(nameof(IdVehiculo))]
    public int IdVehiculo { get; set; }
    [Required]
    [ForeignKey(nameof(IdRuta))]
    public int IdRuta { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionHorario { get; set; }
    /*==============================================================
                    //relaciones
    ================================================================*/
    public Ruta? Ruta { get; set; }
    public Vehiculo? Vehiculo { get; set; }
    public List<Pasaje>? Pasajes { get; set; } = new List<Pasaje>();
    public List<Asiento>? Asientos { get; set; } = new List<Asiento>();
}