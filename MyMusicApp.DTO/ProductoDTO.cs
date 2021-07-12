using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class ProductoDTO : BaseDTO
    {
        [Required(ErrorMessage = "No se puede guardar un producto sino coloca su nombre")]
        [RegularExpression(@"^[A-z][a-z]*$")]
        [MaxLength(100, ErrorMessage = "El campo de nombre del producto no puede ser mayor a 100 caracteres")]
        [Display(Name ="Nombre del producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "Tipo del producto")]
        public int TipoProducto { get; set; }

        [Display(Name = "Cantidad en Inventario")]
        public int CantidadInventario { get; set; }

        [Display(Name = "Precio unitario")]
        [Range(1000, 1000000, ErrorMessage ="Se sobrepasó el precio máximo")]
        public decimal PrecioUnitario { get; set; }

        [Display(Name = "Indicador de Segunda:")]
        public int IndSegunda { get; set; }

        // dd/mm/aaaa hh:mm:ss
        [DisplayFormat(DataFormatString ="{0:yyyy - MM-dd}")]
        public DateTime FechaRegistro { get; set; }

        public SucursalDTO SucursalAsociada { get; set; }

    }
}
