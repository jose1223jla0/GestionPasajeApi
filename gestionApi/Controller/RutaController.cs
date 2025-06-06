﻿using gestionApi.Models;
using gestionApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RutaController : ControllerBase
{
    private readonly IRutaReposiotorio _rutaReposiotorio;

    public RutaController(IRutaReposiotorio rutaReposiotorio)
    {
        _rutaReposiotorio = rutaReposiotorio;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ruta>>> GetRutas()
    {
        IEnumerable<Ruta> rutas = await _rutaReposiotorio.GetRutas();
        return Ok(rutas);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ruta>> GetRuta(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID  del Horario no válido");
        }

        Ruta? ruta = await _rutaReposiotorio.GetRuta(id);

        if (ruta == null)
        {
            return NotFound($"Ruta con ID {id} no encontrado");
        }

        return Ok(ruta);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ruta>> AgregarRuta(Ruta? ruta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (ruta == null)
        {
            return BadRequest("La ruta no puede ser nulo.");
        }
        try
        {
            Ruta nuevaRuta = await _rutaReposiotorio.AgregarRuta(ruta);
            return Ok(nuevaRuta);
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
    public async Task<ActionResult<Ruta>> ActualizarRuta(int id, Ruta ruta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        Ruta? obtenerRuta = await _rutaReposiotorio.GetRuta(id);
        if (obtenerRuta == null)
        {
            return NotFound($"Ruta con ID {id} no encontrado");
        }

        Ruta rutaActualizada = await _rutaReposiotorio.ActualizarRuta(ruta);
        return Ok(rutaActualizada);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Ruta>> BorrarRuta(int id)
    {
        Ruta? existeRuta = await _rutaReposiotorio.GetRuta(id);
        if (existeRuta == null)
        {
            return NotFound($"Ruta con ID {id} no encontrado");
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }
        await _rutaReposiotorio.EliminarRuta(id);
        return Ok(existeRuta);
    }
}