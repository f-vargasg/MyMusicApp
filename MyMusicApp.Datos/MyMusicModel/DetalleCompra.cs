using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class DetalleCompra
    {
        public int PkDetalleOrden { get; set; }
        public int FkProducto { get; set; }
        public int FkOrdenCompra { get; set; }
        public int CntArticulo { get; set; }
        public int IndEstado { get; set; }

        public virtual OrdenCompra FkOrdenCompraNavigation { get; set; }
        public virtual Producto FkProductoNavigation { get; set; }
    }
}
