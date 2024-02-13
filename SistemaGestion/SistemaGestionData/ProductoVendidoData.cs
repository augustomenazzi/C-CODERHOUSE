using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestion.database;

namespace SistemaGestionData
{
    public static class ProductoVendidoData
    {
        public static List<ProductoVendido> ListarProductosVendidos()
        {
            using (CoderContext context = new CoderContext())
            {
                return context.ProductoVendidos.ToList();
            }
        }


        public static ProductoVendido ObtenerProductoVendidoPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                ProductoVendido? productoVendidoBuscado = context.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

                return productoVendidoBuscado;
            }
        }


        public static bool AgregarProductoVendido(ProductoVendido productoVendido)
        {
            using (CoderContext context = new CoderContext())
            {
                context.ProductoVendidos.Add(productoVendido);

                context.SaveChanges();

                return true;
            }
        }


        public static bool ActualizarProductoVentaPorId(ProductoVendido productoVendido, int id)
        {
            using (CoderContext context = new CoderContext())
            {
                ProductoVendido? productoVendidoBuscado = context.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

                productoVendidoBuscado.Stock = productoVendido.Stock;

                context.ProductoVendidos.Update(productoVendido);

                context.SaveChanges();

                return true;
            }
        }


        public static bool EliminarProductoVendidoPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                ProductoVendido? productoVendidoAEliminar = context.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

                if (productoVendidoAEliminar is not null)
                {
                    context.ProductoVendidos.Remove(productoVendidoAEliminar);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }
}
