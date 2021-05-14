using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.DTO
{
    public class VendedorDTO : BaseDTO
    {
        public string NombreVendedor { get; set; }

        public string Puesto { get; set; }

        public SucursalDTO Sucursal { get; set; }

        public string UsuarioVendedor { get; set; }

        public string ClaveVendedor { get; set; }

    }
}
