using gestionApi.Models;
using gestionApi.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ConductorController : ControllerBase
{
    private readonly IConductorRepositorio _repositoryConductores;

    public ConductorController(IConductorRepositorio repositoryConductores)
    {
        _repositoryConductores = repositoryConductores;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Conductor>>> GetConductores()
    {
        IEnumerable<Conductor> conductores = await _repositoryConductores.GetConductores();
        return Ok(conductores);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Conductor>> AgregarConductor([FromBody] Conductor conductor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (conductor == null)
        {
            return BadRequest("El conductor no puede ser nulo.");
        }

        try
        {
            Conductor nuevoConductor = await _repositoryConductores.AgregarConductor(conductor);
            return Ok(nuevoConductor);
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
    public async Task<ActionResult<Conductor>> ActualizarConductor(int id, Conductor conductor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        Conductor? obtenerIdConductor = await _repositoryConductores.GetConductor(id);
        if (obtenerIdConductor == null)
        {
            return NotFound($"Conductor con ID {id} no encontrado");
        }

        Conductor conductorActualizado = await _repositoryConductores.ActualizarConductor(conductor);
        return Ok(conductorActualizado);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Conductor>> GetConductor(int id)
    {
        Conductor? conductor = await _repositoryConductores.GetConductor(id);
        if (id <= 0)
        {
            return BadRequest("ID del conductor no válido");
        }

        if (conductor == null)
        {
            return NotFound($"Conductor con ID {id} no encontrado");
        }

        return Ok(conductor);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Conductor>> BorrarConductor(int id)
    {
        Conductor? conductor = await _repositoryConductores.GetConductor(id);
        if (conductor == null)
        {
            return NotFound($"Conductor con ID {id} no encontrado");
        }

        if (id <= 0)
        {
            return BadRequest("ID del conductor no válido");
        }

        await _repositoryConductores.BorrarConductor(id);
        return Ok(conductor);
    }
}