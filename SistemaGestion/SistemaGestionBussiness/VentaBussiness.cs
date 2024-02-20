using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestionData;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities.Mapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SistemaGestionBussiness
{
    public class VentaBussiness
    {
        private readonly CoderContext coderContext;
        private readonly VentaMapper ventaMapper;
        private readonly ProductoVendidoBussiness productoVendidoBussiness;
        private readonly ProductoBussiness productoBussiness;

        public VentaBussiness(CoderContext coderContext, VentaMapper ventaMapper, ProductoVendidoBussiness productoVendidoBussiness, ProductoBussiness productoBussiness)
        {
            this.coderContext = coderContext;
            this.ventaMapper = ventaMapper;
            this.productoVendidoBussiness = productoVendidoBussiness;
            this.productoBussiness = productoBussiness;
        }


        public List<Venta> ListarVentas()
        {
            return this.coderContext.Venta.ToList();

        }


        public Venta ObtenerVentaPorId(int id)
        {
            Venta? ventaBuscada = this.coderContext.Venta.Where(v => v.Id == id).FirstOrDefault();

            return ventaBuscada;
            
        }


        public List<VentaDTO> ObtenerVentasPorIdUsuario(int idUsuario)
        {
            return this.coderContext.Venta.Where(v => v.IdUsuario == idUsuario).Select(v => this.ventaMapper.MapearADTO(v)).ToList();
        
        }


        public bool AgregarVenta(int idUsuario, List<ProductoDTO> productos) 
        {
            Venta venta = new Venta();

            venta.IdUsuario = idUsuario;

            EntityEntry<Venta>? resultado = this.coderContext.Venta.Add(venta);
            this.coderContext.SaveChanges();

            this.MarcarProductosVendidos(venta.Id, productos);
            this.ActualizarStockProductosVendidos(productos);

            return true;

        }


        public void MarcarProductosVendidos(int idVenta, List<ProductoDTO> productos)
        {
            productos.ForEach(producto =>
            {
                ProductoVendidoDTO productoVendido = new ProductoVendidoDTO();
                productoVendido.IdProducto = producto.Id;
                productoVendido.IdVenta = idVenta;
                productoVendido.Stock = producto.Stock;

                this.productoVendidoBussiness.AgregarProductoVendido(productoVendido);

            });

        }


        public void ActualizarStockProductosVendidos(List<ProductoDTO> productosDTO)
        {
            productosDTO.ForEach(p =>
            {
                ProductoDTO productoActual = this.productoBussiness.ObtenerProductoPorId(p.Id);
                productoActual.Stock -= p.Stock;
                this.productoBussiness.ActualizarProducto(productoActual);
            });
        }


        public bool ActualizarVentaPorId(int id, VentaDTO venta)
        {
            Venta? ventaBuscada = this.coderContext.Venta.Where(v => v.Id == id).FirstOrDefault();

                if (ventaBuscada is not null)
                {
                    ventaBuscada.Comentarios = venta.Comentarios;
                    ventaBuscada.IdUsuario = venta.IdUsuario;

                    this.coderContext.Venta.Update(ventaBuscada);
                    this.coderContext.SaveChanges();
                    return true;
                }

            return false;

        }


        public bool EliminarVentaPorId(int id)
        {
            Venta? ventaAEliminar = this.coderContext.Venta.Include(v => v.ProductoVendidos).Where(v => v.Id == id).FirstOrDefault();

                if (ventaAEliminar is not null)
                {
                    this.coderContext.Venta.Remove(ventaAEliminar);
                    this.coderContext.SaveChanges();
                    return true;
                }

            return false;

        }
    }
}
