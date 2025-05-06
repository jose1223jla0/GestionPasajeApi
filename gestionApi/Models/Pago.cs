using System.ComponentModel.DataAnnotations;

namespace gestionApi.Models;

public class Pago
{
    [Key]
    public int IdPagos { get; set; }
    [Required]
    public decimal MontoPago { get; set; }
    [Required]
    public bool EstadoPago { get; set; }
    /*=============================================================
                    //relaciones
    ===============================================================*/
    public List<Pasaje>? Pasajes { get; set; } = new List<Pasaje>();
}