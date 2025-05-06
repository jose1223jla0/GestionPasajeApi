namespace gestionApi.Models.ModelViewDTO;

public class PasajeroDto
{
    public string? Numero { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Nombres { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public int CodigoVerificacion { get; set; }
    public string? UbigeoSunat { get; set; }
    public string[]? Ubigeo { get; set; }
    public string? Direccion { get; set; }
}
