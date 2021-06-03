using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class ClienteDTO :BaseDTO
    {

        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su identificaicon")]
        [MaxLength(50, ErrorMessage = "El campo de identificación del cliente no puede ser mayor a 50 caracteres")]
        [Display(Name = "Identificación del cliente")]
        public string IdCedula { get; set; }

        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su nombre")]
        [MaxLength(300, ErrorMessage = "El campo de nombre del cliente no puede ser mayor a 300 caracteres")]
        [Display(Name = "Nombre del cliente")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su sexo")]
        [MaxLength(1, ErrorMessage = "El campo de sexo del cliente no puede ser mayor a 1 caracter")]
        [Display(Name = "Sexo del cliente")]
        public string Sexo { get; set; }


        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su fecha de nacimiento")]
        [Display(Name = "Fecha de nacimiento")]
        // dd/mm/aaaa hh:mm:ss
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su usuario")]
        [Display(Name = "Usuario del cliente")]
        public string UsuarioCliente { get; set; }

        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su contraseña")]
        [Display(Name = "Contraseña del cliente")]
        public string IdContrasena { get; set; }

        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su telefono")]
        [MaxLength(8, ErrorMessage = "El campo de telefono del cliente no puede ser mayor a 8 caracteres")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "No se puede guardar un cliente sino coloca su email")]
        [Display(Name = "Email del cliente")]
        public string Email { get; set; }
    }
}
