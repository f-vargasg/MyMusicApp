using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class DetalleSolCompraDTO : BaseDTO
    {
        [Display(Name = "Cantidad Producto")]
        public int CantProducto { get; set; }

        [Display(Name = "Estado de linea producto")]
        public int IndEstado { get; set; }

        [Display(Name = "Porcentaje Descuento")]
        public decimal MtoPctDescuento { get; set; }

        public ProductoDTO ProductoAsociado { get; set; }

        public SolicitudCompraDTO SolicitudCompraAsociada { get; set; }
    }
}
