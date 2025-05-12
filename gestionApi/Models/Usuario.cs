using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionApi.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    [Required]
    public string? NombreUsuario { get; set; }
    [Required]
    public string? Contrasena { get; set; }
    [Required]
    public bool EstadoUsuario { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionUsuario { get; set; }

    /*=================================================================
                   //relaciones
    ===================================================================*/
    public List<Pasaje> Pasajes { get; set; } = new List<Pasaje>();
}
