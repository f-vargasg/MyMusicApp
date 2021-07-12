using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class SolicitudEnvio
    {
        public int PkSolicitudEnvio { get; set; }
        public string DesUbicEnvio { get; set; }
        public DateTime FecEnvio { get; set; }
        public DateTime FecRecibo { get; set; }
        public int FkOrdenCompra { get; set; }
        public int IndEstado { get; set; }
        public decimal MtoPctComision { get; set; }

        public virtual OrdenCompra FkOrdenCompraNavigation { get; set; }
    }
}
