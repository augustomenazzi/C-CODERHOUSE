using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestionData;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities.Mapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SistemaGestionBussiness
{
    public class ProductoVendidoBussiness
    {
        private readonly CoderContext coderContext;
        private readonly ProductoVendidoMapper productoVendidoMapper;
        private readonly ProductoBussiness productoBussiness;
        public ProductoVendidoBussiness(CoderContext coderContext, ProductoVendidoMapper productoVendidoMapper, ProductoBussiness productoBussiness)
        {
            this.coderContext = coderContext;
            this.productoVendidoMapper = productoVendidoMapper;
            this.productoBussiness = productoBussiness;
        }


        public List<ProductoVendido> ListarProductosVendidos()
        {
           return this.coderContext.ProductoVendidos.ToList();
          
        }


        public ProductoVendido ObtenerProductoVendidoPorId(int id)
        {
            ProductoVendido? productoVendidoBuscado = this.coderContext.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

            return productoVendidoBuscado;

        }


        public bool AgregarProductoVendido(ProductoVendidoDTO productoVendido)
        {
            ProductoVendido p = this.productoVendidoMapper.MapearAProductoVendido(productoVendido);
            this.coderContext.ProductoVendidos.Add(p);

            this.coderContext.SaveChanges();

            return true;

        }


        public bool ActualizarProductoVendidoPorId(int id, ProductoVendidoDTO productoVendido)
        {
            ProductoVendido? productoVendidoBuscado = this.coderContext.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

                if (productoVendidoBuscado is not null)
                {
                    productoVendidoBuscado.Stock = productoVendido.Stock;
                    productoVendidoBuscado.IdProducto = productoVendido.IdProducto;
                    productoVendidoBuscado.IdVenta = productoVendido.IdVenta;

                    this.coderContext.ProductoVendidos.Update(productoVendidoBuscado);
                    this.coderContext.SaveChanges();
                    return true;
                }

            return false;

        }


        public bool EliminarProductoVendidoPorId(int id)
        {
            ProductoVendido? productoVendidoAEliminar = this.coderContext.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

                if (productoVendidoAEliminar is not null)
                {
                    this.coderContext.ProductoVendidos.Remove(productoVendidoAEliminar);
                    this.coderContext.SaveChanges();
                    return true;
                }

            return false;

        }


        public List<ProductoVendidoDTO> ObtenerProductosVendidosPorIdUsuario(int idUsuario)
        {
            List<Producto>? productos = this.coderContext.Productos.Include(p => p.ProductoVendidos)
                                                                   .Where(p => p.IdUsuario == idUsuario)
                                                                   .ToList();

            List<ProductoVendido?>? productosVendidos = productos
                                                                .Select(p => p.ProductoVendidos
                                                                .ToList()
                                                                .Find(pv => pv.IdProducto == p.Id))
                                                                .Where(p => !object.ReferenceEquals(p, null))
                                                                .ToList();

            List<ProductoVendidoDTO> dto = productosVendidos.Select(p => this.productoVendidoMapper.MapearADTO(p)).ToList();

            return dto;

        }  
    }
}
