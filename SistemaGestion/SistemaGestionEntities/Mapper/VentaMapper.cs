using SistemaGestionEntities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities.Mapper
{
    public class VentaMapper
    {
        public Venta MapearAVenta(VentaDTO dto)
        {
            Venta venta = new Venta();

            venta.Id = dto.Id;
            venta.Comentarios = dto.Comentarios;
            venta.IdUsuario = dto.IdUsuario;

            return venta;
        }


        public VentaDTO MapearADTO(Venta venta)
        {
            VentaDTO dto = new VentaDTO();

            dto.Id = venta.Id;
            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;

            return dto;
        }
    }
}
