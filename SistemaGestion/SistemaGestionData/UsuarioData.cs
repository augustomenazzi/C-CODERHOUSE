using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestion.database;

namespace SistemaGestionData
{
    public static class UsuarioData
    {
        public static List<Usuario> ListarUsuarios()
        {
            using (CoderContext context = new CoderContext())
            {
                return context.Usuarios.ToList();
            }
        }


        public static Usuario ObtenerUsuarioPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Usuario? usuarioBuscado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                return usuarioBuscado;
            }

        }


        public static bool AgregarUsuario(Usuario usuario)
        {
            using (CoderContext context = new CoderContext())
            {
                context.Usuarios.Add(usuario);

                context.SaveChanges();

                return true;
            }

        }


        public static bool ActualizarUsuarioPorId(Usuario usuario, int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Usuario? usuarioBuscado = context.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                usuarioBuscado.Nombre = usuario.Nombre;
                usuarioBuscado.Apellido = usuario.Apellido;
                usuarioBuscado.NombreUsuario = usuario.NombreUsuario;
                usuarioBuscado.Contraseña = usuario.Contraseña;
                usuarioBuscado.Mail = usuario.Mail;

                context.Usuarios.Update(usuarioBuscado);

                context.SaveChanges(true);

                return true;
            }
        }


        public static bool EliminarUsuarioPorId(int id)
        {
            using (CoderContext context = new CoderContext())
            {
                Usuario? usuarioAEliminar = context.Usuarios.Include(u => u.Productos).Include(u => u.Venta)
                    .Where(u => u.Id == id).FirstOrDefault();

                if (usuarioAEliminar is not null)
                {
                    context.Usuarios.Remove(usuarioAEliminar);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }
}
