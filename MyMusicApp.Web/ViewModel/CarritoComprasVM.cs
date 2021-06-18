using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MyMusicApp.Web.ViewModel
{
    public class CarritoComprasVM
    {
        [Display(Name = "Digite la cantidad de productos que desea comprar")]
        public int CantidadxProducto { get; set; }

        public ProductoDTO ProductoVista { get; set; }

        public List<ProductoDTO> ListaProductosCarrito { get; set; }

        public List<int> ListaCantidadPorProducto { get; set; }

    }
}
