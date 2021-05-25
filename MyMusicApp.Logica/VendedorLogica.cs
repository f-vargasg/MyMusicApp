using MyMusicApp.Datos;
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

        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public VendedorLogica()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static VendedorDTO ConvertirDatosVendedorADTO(Vendedor vendedor)
        {
            return new VendedorDTO
            {
                ClaveVendedor = vendedor.UsrPassword,
                IdEntidad = vendedor.PkVendedor,
                NombreVendedor = vendedor.NomVendedor,
                Puesto = vendedor.DesPuesto,
                UsuarioVendedor = vendedor.UsrVendedor,
                SucursalAsociada = (vendedor.FkSucursalNavigation != null ? SucursalLogica.ConvertirDatosSucursalADTO(vendedor.FkSucursalNavigation) : null)
            };
        }

        internal static Vendedor ConvertirDTOVendedorADatos(VendedorDTO vendedorDTO)
        {
            return new Vendedor
            {
                CodCedula = vendedorDTO.CedulaVendedor,
                DesPuesto = vendedorDTO.Puesto,

            };
        }


        #endregion

        #region Funcion
        public BaseDTO RegistrarVendedor(VendedorDTO vendedor)
        {
            try
            {
                var intermedia = new VendedorDatos(contexto);

                var vendedorDato = ConvertirDTOVendedorADatos(vendedor);

                var resultado = intermedia.AgregarVendedor(vendedorDato);

                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO { Mensaje = resultado.Mensaje + ". Se registraron los datos." };
                }
                else
                {
                    return new ErrorDTO { MensajeError = resultado.Mensaje };
                }
            }
            catch (Exception error)
            {

                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerVendedorPorCodigo(VendedorDTO vendedorDTO)
        {
            try
            {
                VendedorDatos intermDatos = new VendedorDatos(contexto);
                var respuestaDatos = intermDatos.ObtenerVendedorPorCodigo(vendedorDTO.IdEntidad);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var sucursalRespuesta = ConvertirDatosVendedorADTO((Vendedor)respuestaDatos.ContenidoRespuesta);
                    return sucursalRespuesta;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
                    //return (ErrorDTO)respuestaDatos.ContenidoRespuesta; 
                }
            }
            catch (Exception error)
            {

                return new ErrorDTO { MensajeError = error.Message };
            }
        }
        #endregion
        #endregion


    }
}
