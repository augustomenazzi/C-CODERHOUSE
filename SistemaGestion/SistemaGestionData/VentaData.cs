using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestion.database;

namespace SistemaGestionData
{
    public static class VentaData
    {
        public static List<Ventum> ListarVentas()
        {
            using (CoderContext context = new CoderContext())
            {
                return context.Venta.ToList();
            }
        }


        public static Ventum ObtenerVentaPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Ventum? ventaBuscada = context.Venta.Where(v => v.Id == id).FirstOrDefault();

                return ventaBuscada;
            }
        }


        public static bool AgregarVenta(Ventum venta) 
        { 
            using (CoderContext context = new CoderContext())
            {
                context.Venta.Add(venta);

                context.SaveChanges();

                return true;
            }
        }


        public static bool ActualizarVentaPorId(Ventum venta, int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Ventum? ventaBuscada = context.Venta.Where(v => v.Id == id).FirstOrDefault();

                ventaBuscada.Comentarios = venta.Comentarios;

                context.Venta.Update(ventaBuscada);

                context.SaveChanges(); 

                return true;
            }
        }


        public static bool EliminarVentaPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Ventum? ventaAEliminar = context.Venta.Include(v => v.ProductoVendidos).Where(v => v.Id == id).FirstOrDefault();

                if (ventaAEliminar is not null)
                {
                    context.Venta.Remove(ventaAEliminar);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }
}
