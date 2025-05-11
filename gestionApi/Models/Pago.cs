using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Pago
{
    [Key]
    public int IdPago { get; set; }
    [Required]
    public decimal MontoPago { get; set; }
    [Required]
    public bool EstadoPago { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionPago { get; set; }
    /*=============================================================
                    //relaciones
    ===============================================================*/
    public List<Pasaje>? Pasajes { get; set; } = new List<Pasaje>();
}