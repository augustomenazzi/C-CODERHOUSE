using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestionData;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities.Mapper;

namespace SistemaGestionBussiness
{
    public class ProductoBussiness
    {
        private readonly CoderContext coderContext;
        private readonly ProductoMapper productoMapper;
        public ProductoBussiness(CoderContext coderContext, ProductoMapper productoMapper)
        {
            this.coderContext = coderContext;
            this.productoMapper = productoMapper;
        }


        public List<Producto> ListarProductos()
        {
            return this.coderContext.Productos.ToList();

        }


        public ProductoDTO ObtenerProductoPorId(int id)
        {
            ProductoDTO? productoBuscado = this.coderContext.Productos.Where(p => p.Id == id).Select(p => this.productoMapper.MapearADTO(p)).FirstOrDefault();

            return productoBuscado;

        }


        public bool AgregarProducto(ProductoDTO producto)
        {
            Producto p = this.productoMapper.MapearAProducto(producto);

            this.coderContext.Productos.Add(p);
            this.coderContext.SaveChanges();
            return true;

        }


        public bool ActualizarProducto(ProductoDTO producto)
        {
            Producto? productoBuscado = this.coderContext.Productos.Where(p => p.Id == producto.Id).FirstOrDefault();

                if (productoBuscado is not null)
                {
                    productoBuscado.Descripciones = producto.Descripciones;
                    productoBuscado.Costo = producto.Costo;
                    productoBuscado.PrecioVenta = producto.PrecioVenta;
                    productoBuscado.Stock = producto.Stock;
                    productoBuscado.IdUsuario = producto.IdUsuario;

                    this.coderContext.Productos.Update(productoBuscado);
                    this.coderContext.SaveChanges();
                    return true;
                }

            return false;

        }


        public bool EliminarProductoPorId(int id)
        {
            Producto? productoAEliminar = this.coderContext.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

                if (productoAEliminar is not null)
                {
                    this.coderContext.Productos.Remove(productoAEliminar);
                    this.coderContext.SaveChanges();
                    return true;
                }
                
            return false;
                                
        }


        public List<ProductoDTO> ObtenerListaDeProductosDTO()
        {
            var productosDTO = new List<ProductoDTO>();
            var productos = this.coderContext.Productos.Select(p=> this.productoMapper.MapearADTO(p)).ToList();

            return productosDTO;

        }


        public List<ProductoDTO> OtenerProductosPorIdUsuario(int idUsuario)
        {
           return this.coderContext.Productos.Where(p => p.IdUsuario == idUsuario).Select(p => this.productoMapper.MapearADTO(p)).ToList();
       
        }
    }
}
