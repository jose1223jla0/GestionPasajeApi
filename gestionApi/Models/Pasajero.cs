using System.ComponentModel.DataAnnotations;

namespace gestionApi.Models;

public class Pasajero
{
    [Key]
    public int IdPasajero { get; set; }
    [Required]
    public string? NombrePasajero { get; set; }
    [Required]
    public string? ApellidoPasajero { get; set; }
    [Required]
    public string? DniPasajero { get; set; }
    [Required]
    public string? TelefPasajero { get; set; }
    /*=============================================================
                        // Relaciones
    ==============================================================*/
    public List<Pasaje>? Pasajes { get; set; }= new List<Pasaje>();
}