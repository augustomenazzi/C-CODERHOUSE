using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using SistemaGestionData;
using SistemaGestionEntities.DTO_s;
using SistemaGestionEntities.Mapper;
using System.Linq;

namespace SistemaGestionBussiness
{
    public class UsuarioBussiness
    {
        private readonly CoderContext coderContext;
        private readonly UsuarioMapper usuarioMapper;
        public UsuarioBussiness(CoderContext coderContext, UsuarioMapper usuarioMapper)
        {
            this.coderContext = coderContext;
            this.usuarioMapper = usuarioMapper;
        }


        public List<Usuario> ListarUsuarios()
        {
            return this.coderContext.Usuarios.ToList();

        }


        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario? usuarioBuscado = this.coderContext.Usuarios.Where(u => u.Id == id).FirstOrDefault();

            return usuarioBuscado;

        }


        public UsuarioDTO ObtenerUsuarioPorNombreUsuario(string nombreUsuario) 
        {
            UsuarioDTO? p = this.coderContext.Usuarios.Where(u => u.NombreUsuario == nombreUsuario)
                                                     .Select(u => this.usuarioMapper.MapearADTO(u))
                                                     .FirstOrDefault();
            
            return p;
        
        }


        public UsuarioDTO ObtenerUsuarioPorUsuarioYPassword(string usuario, string password)
        {
            UsuarioDTO? usuarioBuscado = this.coderContext.Usuarios.Where(u => string.Equals(u.NombreUsuario, usuario, StringComparison.Ordinal))
                                                             .Select(u => this.usuarioMapper.MapearADTO(u))
                                                             .FirstOrDefault();

            if (usuarioBuscado != null)
            {
                if (string.Equals(usuarioBuscado.Contraseña, password, StringComparison.Ordinal))
                {
                    return usuarioBuscado;

                }
            }
            return null;

        }


        public bool AgregarUsuario(UsuarioDTO usuario)
        {
            Usuario u = this.usuarioMapper.MapearAUsuario(usuario);

            this.coderContext.Usuarios.Add(u);

            this.coderContext.SaveChanges();

            return true;

        }


        public bool ActualizarUsuarioPorId(int id, UsuarioDTO usuario)
        {
            Usuario? usuarioBuscado = this.coderContext.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                if (usuarioBuscado is not null)
                {
                    usuarioBuscado.Nombre = usuario.Nombre;
                    usuarioBuscado.Apellido = usuario.Apellido;
                    usuarioBuscado.NombreUsuario = usuario.NombreUsuario;
                    usuarioBuscado.Contraseña = usuario.Contraseña;
                    usuarioBuscado.Mail = usuario.Mail;

                    this.coderContext.Usuarios.Update(usuarioBuscado);
                    this.coderContext.SaveChanges(true);
                    return true;
                }

            return false;

        }


        public bool EliminarUsuarioPorId(int id)
        {
            Usuario? usuarioAEliminar = this.coderContext.Usuarios.Include(u => u.Productos).Include(u => u.Venta)
                    .Where(u => u.Id == id).FirstOrDefault();

                if (usuarioAEliminar is not null)
                {
                    this.coderContext.Usuarios.Remove(usuarioAEliminar);
                    this.coderContext.SaveChanges();
                    return true;
                }    

            return false;
        
        }
    }
}
