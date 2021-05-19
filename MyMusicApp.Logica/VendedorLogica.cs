using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Logica
{
    public class VendedorLogica
    {
        internal static VendedorDTO ConvertirDatosVendedorADTO(Vendedor vendedor)
        {
            return new VendedorDTO
            {
                ClaveVendedor = vendedor.UsrPassword,
                IdEntidad = vendedor.PkVendedor,
                NombreVendedor = vendedor.NomVendedor,
                Puesto = vendedor.DesPuesto,
                UsuarioVendedor = vendedor.UsrVendedor,
                SucursalAsociada = (vendedor.FkSucursalNavigation != null ? SucursalLogica.ConvertirDatosSucursalADTO( vendedor.FkSucursalNavigation) : null)
            };
        }

        internal static Vendedor ConvertirDTOVendedorADatos(VendedorDTO vendedorDTO)
        {
            return new Vendedor
            {
                CodCedula = vendedorDTO.CedulaVendedor,
                DesPuesto =vendedorDTO.Puesto,
                
            };
        }


    }
}
