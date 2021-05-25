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
    public class SucursalLogica
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public SucursalLogica()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static SucursalDTO ConvertirDatosSucursalADTO(Sucursal sucursal)
        {
            return new SucursalDTO
            {
                CorreoElectronico = sucursal.EmlSucursal,
                DirSucursal = sucursal.DirUbicacion,
                HorarioSucursal = sucursal.DesHorario,
                IdEntidad = sucursal.PkSucursal,
                TelefonoSucursal = sucursal.TelSucursal
            };
        }

        internal static Sucursal ConvertirSucursalDTOaDatos(SucursalDTO sucursalDTO)
        {
            return new Sucursal
            {
                DesHorario = sucursalDTO.HorarioSucursal,
                DirUbicacion = sucursalDTO.DirSucursal,
                EmlSucursal = sucursalDTO.CorreoElectronico,
                TelSucursal = sucursalDTO.TelefonoSucursal
            };
        }

        #endregion

        #region Funcion
        public BaseDTO ObtenerSucursalPorCodigo(int codigoSucursal)
        {
            try
            {
                // Si estoy seguro que voy a utilizar la clase de sucursales, puedo dejar el contexto vació. 

                SucursalDatos intermedioEjemplo = new SucursalDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerSucursalPorCodigo(codigoSucursal);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);

                    return sucursalRespuesta;
                }
                else
                {
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }

            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerSucursalPorUbicacion(string ubicacion)
        {
            try
            {
                // Si estoy seguro que voy a utilizar la clase de sucursales, puedo dejar el contexto vació. 

                SucursalDatos intermedioEjemplo = new SucursalDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerSucursalPorUbicacion(ubicacion);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);

                    return sucursalRespuesta;
                }
                else
                {
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }

            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO AgregarSucursal(string ubicacion, string horario, string telefono, string correo)
        {
            try
            {

                SucursalDatos intermedioDatos = new SucursalDatos(contexto);

                var sucursalDato = new SucursalDTO
                {
                    CorreoElectronico = correo,
                    DirSucursal = ubicacion,
                    HorarioSucursal = horario,

                };

                var resultado = intermedioDatos.AgregarSucursal(ubicacion, horario, telefono, correo);


                if (resultado.CodigoRespuesta != -1)
                {
                    // caso exito
                    return new BaseDTO
                    {
                        Mensaje = resultado.Mensaje + "Se actualizo un total de " + resultado.ContenidoRespuesta + " sucursales."
                    };

                }
                else
                {
                    //error controlado
                    //throw new Exception(((ErrorDTO)resultado.ContenidoRespuesta).mensajeError);
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }

            }
            catch (Exception error)
            {

                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ActualizarDatosSucursal(int codigo, string ubicacion, string telefono, string correo,
                                      string horario)
        {
            try
            {
                var intermedia = new SucursalDatos(contexto);
                var resultado = intermedia.ActualizarDatosSucursal(codigo, ubicacion, telefono, correo, horario);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Los datos de la sucursal correctamente " 
                    };
                }
                else
                {
                    return (ErrorDTO)resultado.ContenidoRespuesta;
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
