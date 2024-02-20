using SistemaGestionEntities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities.Mapper
{
    public class ProductoVendidoMapper
    {
        public ProductoVendido MapearAProductoVendido(ProductoVendidoDTO dto)
        {
            ProductoVendido productoVendido = new ProductoVendido();

            productoVendido.Id = dto.Id;
            productoVendido.Stock = dto.Stock;
            productoVendido.IdProducto = dto.IdProducto;
            productoVendido.IdVenta = dto.IdVenta;

            return productoVendido;
        }


        public ProductoVendidoDTO MapearADTO(ProductoVendido productoVendido) 
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();

            dto.Id = productoVendido.Id;
            dto.Stock = productoVendido.Stock;
            dto.IdProducto = productoVendido.IdProducto;
            dto.IdVenta = productoVendido.IdVenta;

            return dto;
        }

    }
}
