using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities;
using SistemaGestionBussiness;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductoVendidoController : Controller
    {
        private readonly ProductoVendidoBussiness productoVendidoBussiness;
        public ProductoVendidoController(ProductoVendidoBussiness productoVendidoBussiness)
        {
            this.productoVendidoBussiness = productoVendidoBussiness;

        }


        [HttpGet("{idUsuario}")]
        public ActionResult<List<ProductoVendidoDTO>> ObtenerProductosVendidosPorIdUsuario(int idUsuario)
        {
            if (idUsuario < 0) 
            {
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
            try
            {
                return this.productoVendidoBussiness.ObtenerProductosVendidosPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = ex.Message });
            }
        }
    }
}