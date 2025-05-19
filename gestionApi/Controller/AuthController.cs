using gestionApi.Dto;
using gestionApi.Repository.Interface;
using gestionApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace gestionApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepositorio _authRepositorio;
        private readonly IJwtServicio _jwtServicio;

        public AuthController(IAuthRepositorio authRepositorio, IJwtServicio jwtServicio)
        {
            _authRepositorio = authRepositorio;
            _jwtServicio = jwtServicio;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("El nombre de usuario o la contraseña no pueden estar vacíos.");
            }

            try
            {
                var usuarioDb = await _authRepositorio.Login(loginDto.Username, loginDto.Password);
                string token = _jwtServicio.CrearToken(usuarioDb);
                return Ok(new
                {
                    mensaje = "Usuario logueado correctamente.",
                    token
                });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
            }
        }

    }
}
