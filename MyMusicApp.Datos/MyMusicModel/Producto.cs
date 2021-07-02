using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public int PkProducto { get; set; }
        public string NomProducto { get; set; }
        public int? TipProducto { get; set; }
        public int? CntProducto { get; set; }
        public int? FkSucursal { get; set; }
        public decimal? MtoPrecioUnitario { get; set; }
        public int? IndSegunda { get; set; }

        public virtual Sucursal FkSucursalNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
