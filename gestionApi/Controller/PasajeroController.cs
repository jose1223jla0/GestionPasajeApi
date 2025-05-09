using gestionApi.Models;
using gestionApi.Models.ModelViewDTO;
using gestionApi.Repository.Interface;
using gestionApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class PasajeroController : ControllerBase
{
    private readonly IPasajeroRepositorio _repositoryPasajeroRepositorio;
    private readonly IPasajeroService _pasajeroService;


    public PasajeroController(IPasajeroRepositorio repositoryPasajeroRepositorio, IPasajeroService pasajeroService)
    {
        _repositoryPasajeroRepositorio = repositoryPasajeroRepositorio;
        _pasajeroService = pasajeroService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Pasajero>>> GetPasajeros()
    {
        IEnumerable<Pasajero> pasajeros = await _repositoryPasajeroRepositorio.GetPasajeros();
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
    public async Task<ActionResult<Pasajero>> AgregarPasajero(Pasajero pasajero)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _repositoryPasajeroRepositorio.InsertarPasajero(pasajero);
        return Ok(pasajero);
    }
}