using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class SolicitudEnvioDTO
    {

        [Required(ErrorMessage = "No se puede guardar una Solicitud de Envio sin ubicacion")]
        [Display(Name = "Ubicacioin de Envio")]
        public string UbicacionEnvio { get; set; }

        [Required(ErrorMessage = "No se puede guardar una Solicitud de Envio sin Fecha de Envio")]
        [Display(Name = "Fecha de Envio")]
        public DateTime FecEnvio { get; set; }

        [Required(ErrorMessage = "No se puede guardar una Solicitud de Envio sin Fecha de Recibido")]
        [Display(Name = "Fecha de Recibido")]
        public DateTime FechaRecibido { get; set; }

        public SolicitudCompraDTO OrdenCompraAsociada { get; set; }

        [Required(ErrorMessage = "No se puede guardar una Solicitud de Envio sin Estado solicitud")]
        [Display(Name = "Estado Solicitud")]
        public int EstadoSolicEnvio { get; set; }

    }
}
