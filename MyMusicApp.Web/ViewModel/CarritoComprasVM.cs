using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyMusicApp.Web.ViewModel
{
    public class CarritoComprasVM
    {
        
        public int IdentificadorProducto { get; set; }

        public int CantidadxProducto { get; set; }

        public int TotalProductos { get; set; }

        public ProductoDTO ProductoVista { get; set; }

        public List<ProductoDTO> ListProductosCarrito;

        public List<int> ListaCantidadPorProducto { get; set; }

        // public double MontoTotal { get; set; } se va a meter en un viewBag
    }
}
