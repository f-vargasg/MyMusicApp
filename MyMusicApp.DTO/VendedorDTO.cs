using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class VendedorDTO : BaseDTO
    {

        [Required(ErrorMessage = "No se puede guardar un vendedor sino coloca su cedula")]
        [MaxLength(80, ErrorMessage = "El campo de cedula del Vendedor no puede ser mayor a 80 caracteres")]
        [Display(Name = "Cedula del vendedor")]
        public string CedulaVendedor { get; set; }

        [Required(ErrorMessage = "No se puede guardar un vendedor sino coloca su nombre")]
        [MaxLength(80, ErrorMessage = "El campo de nombre del Vendedor no puede ser mayor a 80 caracteres")]
        [Display(Name = "Nombre del vendedor")]
        public string NombreVendedor { get; set; }

        [Required(ErrorMessage = "No se puede guardar un producto sino coloca su puesto")]
        [MaxLength(100, ErrorMessage = "El campo de puesto del Vendedor no puede ser mayor a 100 caracteres")]
        [Display(Name = "Nom.Puesto del Vendedor")]
        public string Puesto { get; set; }
        public SucursalDTO SucursalAsociada { get; set; }

        [Required(ErrorMessage = "No se puede guardar un vendedor sino coloca su usuario")]
        public string UsuarioVendedor { get; set; }

        [Required(ErrorMessage = "No se puede guardar un vendedor sino coloca su contraseña")]
        public string ClaveVendedor { get; set; }

    }
}
