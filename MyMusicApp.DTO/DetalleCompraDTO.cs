using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class DetalleCompraDTO : BaseDTO
    {

        public int CantidadProducto { get; set; }

        public int Estado { get; set; }

        public ProductoDTO ProductoAsociado { get; set; }

        public SolicitudCompraDTO SolicitudCompraAsociada { get; set; }

    }
}
