using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class SolicitudCompraDTO : BaseDTO
    {

        [Required(ErrorMessage = "No se puede guardar una Solicitud de compra sin fecha")]
        [Display(Name = "Fecha de la orden")]
        public DateTime FechaOrden { get; set; }

        [Required(ErrorMessage = "No se puede guardar una Solicitud de compra sin Tipo de Entrega")]
        [Display(Name = "Tipo de Entrega")]
        public int TipoEntrega { get; set; }

        [Required(ErrorMessage = "No se puede guardar una Solicitud de compra sin Monto Total")]
        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }

        public decimal PctDescuento { get; set; }

        public VendedorDTO VendedorAsociado { get; set; }

        public ClienteDTO ClienteAsociado { get; set; }

        public int EstadoSolicitud { get; set; }

        public decimal MtoPctDescuento { get; set; }

    }
}
