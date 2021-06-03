using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class SucursalDTO : BaseDTO
    {
        [Display(Name = "Ubicacion de la sucursal:")]
        [MaxLength(300, ErrorMessage = "El campo de ubicación no puede ser superior a 300 caracteres")]
        public string DirSucursal { get; set; }

        [Display(Name = "Horario de atención:")]
        [MaxLength(5, ErrorMessage = "El campo de Horario no puede ser superior a 5 caracteres")]
        public string HorarioSucursal { get; set; }

        [Display(Name = "Telefono:")]
        [MaxLength(10, ErrorMessage = "El teléfono de la sucursal no puede tener mas 10 caracteres")]
        public string TelefonoSucursal { get; set; }


        [EmailAddress(ErrorMessage = "Debe especificar una dirección email correcta")]
        [MaxLength(50, ErrorMessage = "El email de la sucursal no puede tener mas 60 caracteres")]
        public string CorreoElectronico { get; set; }



    }
}
