using SistemaGestionEntities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities.Mapper
{
    public class UsuarioMapper
    {
        public Usuario MapearAUsuario(UsuarioDTO dto)
        {
            Usuario usuario = new Usuario();

            usuario.Id = dto.Id;
            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Contraseña = dto.Contraseña;
            usuario.Mail = dto.Mail;

            return usuario;
        }
        

        public UsuarioDTO MapearADTO(Usuario usuario)
        {
            UsuarioDTO dto = new UsuarioDTO();

            dto.Id = usuario.Id;
            dto.Nombre = usuario.Nombre;
            dto.Apellido = usuario.Apellido;
            dto.NombreUsuario = usuario.NombreUsuario;
            dto.Contraseña = usuario.Contraseña;
            dto.Mail = usuario.Mail;

            return dto;
        }
    }
}
