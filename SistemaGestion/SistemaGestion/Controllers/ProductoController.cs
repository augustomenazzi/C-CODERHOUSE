using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities;
using SistemaGestionBussiness;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductoController : Controller
    {
        private readonly ProductoBussiness productoBussiness;
        public ProductoController(ProductoBussiness productoBussiness)
        {
            this.productoBussiness = productoBussiness;
        
        }


        [HttpGet("Obtener productos por {idUsuario}")]
        public ActionResult<List<ProductoDTO>> ObtenerProductosPorIdUsuario(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
            try
            {
                return this.productoBussiness.OtenerProductosPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = ex.Message});
            }

        }


        [HttpPost("Agregar un producto")]
        public IActionResult AgregarUnNuevoProducto([FromBody] ProductoDTO producto)
        {
            if (this.productoBussiness.AgregarProducto(producto))
            {
                return base.Ok(new { mensaje = "Producto agregado", producto });
            }
            else
            {
                return base.Conflict(new { mensaje = "No se agrego el producto" });
            }
        }


        [HttpPut("Actualizar un producto")]
        public IActionResult ActualizarProducto(ProductoDTO producto)
        {
            try
            {
                this.productoBussiness.ActualizarProducto(producto);
                return base.Ok(new { mensaje = "Producto actualizado" });

            }
            catch (Exception ex)
            {
                return base.BadRequest(ex);
            }
        }


        [HttpDelete("Eliminar producto por {id}")]
        public IActionResult BorrarProducto(int id)
        {
            if (id > 0)
            {
                if (this.productoBussiness.EliminarProductoPorId(id))
                {
                    return base.Ok(new { mensaje = "Producto eliminado" });
                }
                return base.Conflict(new { mensaje = "No se pudo eliminar el producto" });

            }
            return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
        }
    }
}
