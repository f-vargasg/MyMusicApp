using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApp.Web.ViewModel
{
    public class SolicitudesCompraVM
    {
        public SolicitudCompraDTO SolicitudCompra { get; set; }

        public List<SolicitudCompraDTO> ListadoSolicitudesCompra { get; set; }
        public SucursalDTO Sucursal { get; set; }
        public ProductoDTO Producto { get; set; }
        public List<ProductoDTO> ListadoProductos { get; set; }
        public ErrorDTO Error { get; set; }
    }
}
