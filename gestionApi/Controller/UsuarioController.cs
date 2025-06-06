using gestionApi.Models;
using gestionApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace gestionApi.Controller;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        IEnumerable<Usuario> resultado = await _usuarioRepositorio.GetUsuarios();
        return Ok(resultado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Usuario>> AgregarUsuario(Usuario? usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (usuario == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        try
        {
            Usuario nuevoUsuario = await _usuarioRepositorio.AgregarUsuario(usuario);
            return Ok(nuevoUsuario);
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Usuario>> EditarUsuario(int id, Usuario? usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        if (usuario == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        try
        {
            Usuario usuarioEditado = await _usuarioRepositorio.EditarUsuario(usuario);
            return Ok(usuarioEditado);
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
}
