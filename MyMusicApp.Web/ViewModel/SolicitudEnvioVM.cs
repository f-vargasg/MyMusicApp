using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApp.Web.ViewModel
{
    public class SolicitudEnvioVM
    {
        public List<ProductoDTO> ListadoProductos { get; set; }

        public SolicitudEnvioDTO SolicitudEnvio { get; set; }

        public SucursalDTO Sucursal { get; set; }

        public SolicitudCompraDTO SolicitudCompra { get; set; }

        public ClienteDTO Cliente { get; set; }

        public List<SolicitudEnvioDTO> ListadoSolicitudes { get; set; }

        public ErrorDTO Error { get; set; }
    }
}
