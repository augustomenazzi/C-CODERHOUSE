using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities;
using SistemaGestionBussiness;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class VentaController : Controller
    {
        private readonly VentaBussiness ventaBussiness;
        public VentaController(VentaBussiness ventaBussiness)
        {
            this.ventaBussiness = ventaBussiness;
        }


        [HttpGet("{idUsuario}")]
        public ActionResult<List<VentaDTO>> ObtenerVentasPorIdUsuario(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
            try
            {
                return this.ventaBussiness.ObtenerVentasPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = ex.Message });
            }
        }


        [HttpPost ("{idUsuario}")]
        public IActionResult AgregarUnaNuevaVenta(int idUsuario, [FromBody] List<ProductoDTO> productos)
        {
            if (productos.Count == 0)
            {
                return base.Conflict(new { mensaje = "No se recibieron productos" });
            }
            try
            {
                this.ventaBussiness.AgregarVenta(idUsuario, productos);
                return base.Ok(new { mensaje = "Venta realizada" });
            }
            catch (Exception ex)
            {
                return base.BadRequest(ex);
            }
        }
    }
}
