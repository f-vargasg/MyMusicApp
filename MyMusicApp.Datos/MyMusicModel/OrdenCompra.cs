using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class OrdenCompra
    {
        public OrdenCompra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            SolicitudEnvioDomics = new HashSet<SolicitudEnvioDomic>();
        }

        public int PkOrdenCompra { get; set; }
        public DateTime FecOrden { get; set; }
        public int TipEntrega { get; set; }
        public decimal MntTotalOrden { get; set; }
        public int FkVendedor { get; set; }
        public int FkCliente { get; set; }
        public int IndEstado { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual Vendedor FkVendedorNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<SolicitudEnvioDomic> SolicitudEnvioDomics { get; set; }
    }
}
