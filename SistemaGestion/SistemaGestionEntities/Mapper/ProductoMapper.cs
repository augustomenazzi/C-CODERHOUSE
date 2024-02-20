using SistemaGestionEntities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities.Mapper
{
    public class ProductoMapper
    {
        public Producto MapearAProducto(ProductoDTO dto)
        {
            Producto producto = new Producto();

            producto.Id = dto.Id;
            producto.Descripciones = dto.Descripciones;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.Costo = dto.Costo;
            producto.Stock = dto.Stock;
            producto.IdUsuario = dto.IdUsuario;

            return producto;
        }


        public ProductoDTO MapearADTO(Producto producto) 
        {
            ProductoDTO dto = new ProductoDTO();

            dto.Id = producto.Id;
            dto.Descripciones = producto.Descripciones;
            dto.PrecioVenta = producto.PrecioVenta;
            dto.Costo = producto.Costo;
            dto.Stock = producto.Stock;
            dto.IdUsuario = producto.IdUsuario;

            return dto;
        }
    }
}
