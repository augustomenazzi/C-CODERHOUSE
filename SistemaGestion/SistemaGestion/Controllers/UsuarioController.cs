using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities;
using SistemaGestionBussiness;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : Controller
    {
        private readonly UsuarioBussiness usuarioBussiness;
        public UsuarioController(UsuarioBussiness usuarioBussiness)
        {
            this.usuarioBussiness = usuarioBussiness;
        }


        [HttpGet("Obtener Usuario por {nombreUsuario}")]
        public ActionResult<UsuarioDTO> ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return base.Conflict(new { mensaje = "Escribi algo dale" });
            }
            try
            {
                UsuarioDTO usuario = this.usuarioBussiness.ObtenerUsuarioPorNombreUsuario(nombreUsuario);
                return usuario;
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = ex.Message });
            }

        }


        [HttpGet("Obtener Usuario por {usuario}/{password}")]
        public ActionResult<UsuarioDTO> ObtenerUsuarioPorUsuarioYPassword(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                return base.Conflict(new { mensaje = "Escribi algo dale" });
            }
            try
            {
                return this.usuarioBussiness.ObtenerUsuarioPorUsuarioYPassword(usuario, password);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = ex.Message });
            }
        }



        [HttpPost("Agregar un Usuario")]
        public IActionResult AgregarUnNuevoUsuario([FromBody] UsuarioDTO usuario)
        {
            if (this.usuarioBussiness.AgregarUsuario(usuario))
            {
                return base.Ok(new { mensaje = "Usuario agregado", usuario });
            }
            else
            {
                return base.Conflict(new { mensaje = "No se agrego el usuario" });
            }
        }


        [HttpPut("Actualizar Usuario por {id}")]
        public IActionResult ActualizarUsuarioPorId(int id, UsuarioDTO usuario)
        {
            if (id > 0)
            {
                if (this.usuarioBussiness.ActualizarUsuarioPorId(id, usuario))
                {
                    return base.Ok(new { mensaje = "Usuario actualizado" });
                }
                return base.Conflict(new { mensaje = "No se pudo actualizar el usuario" });
            }
            return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
        }
    }
}
