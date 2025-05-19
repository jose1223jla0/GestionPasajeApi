using gestionApi.Models;
using gestionApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehiculoController : ControllerBase
{
    private readonly IVehiculoRepositorio _vehiculoRepositorio;

    public VehiculoController(IVehiculoRepositorio vehiculoRepositorio)
    {
        _vehiculoRepositorio = vehiculoRepositorio;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
    {
        IEnumerable<Vehiculo> vehiculos = await _vehiculoRepositorio.GetVehiculos();
        return Ok(vehiculos);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
    {
        Vehiculo? existeVehiculo = await _vehiculoRepositorio.GetVehiculo(id);
        if (existeVehiculo == null)
        {
            return NotFound($"Vehiculo con ID {id} no encontrado");
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        return Ok(existeVehiculo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vehiculo>> AgregarVehiculo(Vehiculo? vehiculo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (vehiculo == null)
        {
            return BadRequest("El vehiculo no puede ser nulo");
        }
        try
        {
            Vehiculo nuevoVehiculo = await _vehiculoRepositorio.AgregarVehiculo(vehiculo);
            return Ok(nuevoVehiculo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vehiculo>> ActualizarVehiculo(int id, Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        Vehiculo? obtenerIdVehiculo = await _vehiculoRepositorio.GetVehiculo(id);
        if (obtenerIdVehiculo == null)
        {
            return NotFound($"Vehiculo con ID {id} no encontrado");
        }

        var vehiculoActualizado = await _vehiculoRepositorio.ActualizarVehiculo(vehiculo);
        return Ok(vehiculoActualizado);
    }
}