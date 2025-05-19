using gestionApi.Models;
using gestionApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HorarioController : ControllerBase
{
    private readonly IHorarioRepositorio _horarioRepositorio;

    public HorarioController(IHorarioRepositorio horarioRepositorio)
    {
        _horarioRepositorio = horarioRepositorio;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
    {
        IEnumerable<Horario> horarios = await _horarioRepositorio.GetHorarios();
        return Ok(horarios);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Horario>> GetHorario(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID del Horario no válido");
        }

        Horario? horario = await _horarioRepositorio.GetHorario(id);
        if (horario == null)
        {
            return NotFound($"Horario con ID {id} no encontrado");
        }

        return Ok(horario);
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Horario>> ActualizarHorario(int id, Horario horario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        Horario? otenerIdHorario = await _horarioRepositorio.GetHorario(id);
        if (otenerIdHorario == null)
        {
            return NotFound($"Horario con ID {id} no encontrado");
        }

        Horario horarioActualizado = await _horarioRepositorio.ActualizarHorario(horario);
        return Ok(horarioActualizado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Horario>> AgregarHorario(Horario horario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Horario horarioAgregado = await _horarioRepositorio.AgregarHorario(horario);
        return Ok(horarioAgregado);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Horario>> BorrarHorario(int id)
    {
        Horario? existeHorario = await _horarioRepositorio.GetHorario(id);
        if (existeHorario == null)
        {
            return NotFound($"Horario con ID {id} no encontrado");
        }

        if (id <= 0)
        {
            return BadRequest("ID del Horario no válido");
        }

        await _horarioRepositorio.BorrarHorario(id);
        return Ok(existeHorario);
    }
}