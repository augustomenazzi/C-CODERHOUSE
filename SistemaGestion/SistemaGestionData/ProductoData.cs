using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestion.database;


namespace SistemaGestionData
{
    public static class ProductoData
    {
        public static List<Producto> ListarProductos()
        {
            using(CoderContext context = new CoderContext())
            {
                return context.Productos.ToList();
            }
        }


        public static Producto ObtenerProductoPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Producto? productoBuscado = context.Productos.Where(p => p.Id == id).FirstOrDefault();

                return productoBuscado;
            }

        }


        public static bool AgregarProducto(Producto producto)
        {
            using (CoderContext context = new CoderContext())
            {
                context.Productos.Add(producto);

                context.SaveChanges();

                return true;

            }

        }


        public static bool ActualizarProductoPorId(Producto producto, int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Producto? productoBuscado = context.Productos.Where(p => p.Id == id).FirstOrDefault();

                productoBuscado.Descripciones = producto.Descripciones;
                productoBuscado.Costo = producto.Costo;
                productoBuscado.PrecioVenta = producto.PrecioVenta;
                productoBuscado.Stock = producto.Stock;

                context.Productos.Update(productoBuscado);

                context.SaveChanges(true);

                return true;

            }
        }


        public static bool EliminarProductoPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Producto? productoAEliminar = context.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

                if (productoAEliminar is not null)
                {
                    context.Productos.Remove(productoAEliminar);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }
}
