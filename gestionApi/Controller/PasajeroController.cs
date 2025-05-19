using gestionApi.Models;
using gestionApi.Models.ModelViewDTO;
using gestionApi.Repository.Interface;
using gestionApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PasajeroController : ControllerBase
{
    private readonly IPasajeroRepositorio _pasajeroRepositorio;
    private readonly IPasajeroService _pasajeroService;


    public PasajeroController(IPasajeroRepositorio pasajeroRepositorio, IPasajeroService pasajeroService)
    {
        _pasajeroRepositorio = pasajeroRepositorio;
        _pasajeroService = pasajeroService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Pasajero>>> GetPasajeros()
    {
        IEnumerable<Pasajero> pasajeros = await _pasajeroRepositorio.GetPasajeros();
        return Ok(pasajeros);
    }

    [HttpGet]
    [Route("{dni}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PasajeroDto>> BuscarPorDni(string dni)
    {

        PasajeroDto? pasajero = await _pasajeroService.BuscarPorDni(dni);
        if (pasajero == null)
        {
            return NotFound($"Pasajero con DNI {dni} no encontrado");
        }

        return Ok(pasajero);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pasajero>> AgregarPasajero(Pasajero? pasajero)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (pasajero == null)
        {
            return BadRequest("El conductor no puede ser nulo");
        }

        try
        {
            Pasajero nuevoPasajero = await _pasajeroRepositorio.AgregarPasajero(pasajero);
            return Ok(nuevoPasajero);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado" });
        }
    }
}