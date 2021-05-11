using System;
using MyMusicApp.Datos;
using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;

namespace MyMusicApp.Logica
{
    public class ClaseEjemploLogica
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public ClaseEjemploLogica()
        {

        }
        #endregion


        #region Metodos
        #region Conversiones 
        // Entidad Convertida                   // entidad a convertir    
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

        #region Funciones
        public BaseDTO ObtenerSucursalPorCodigo (int codigoSucursal)
        {
            try
            {
                // ClaseEjemploDatos intermedioEjemplo = new ClaseEjemploDatos(this.contexto);  // Este se puede usar tambien
                ClaseEjemploDatos intermedioEjemplo = new ClaseEjemploDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerProductoPorCodigo(codigoSucursal);
                // Caso Exitoso
                if (respuestaDatos.CodigoRespuesta == 1) // ok
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);

                    return sucursalRespuesta;
                }
                else 
                {
                    // excepcion controlada
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }

            }
            catch (Exception error )
            {
                // excepciones no controladas
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerSucursalPorCodigo(SucursalDTO sucursalDTO)
        {
            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ObtenerProductoPorCodigo(sucursalDTO.IdEntidad);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);
                    return sucursalRespuesta;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
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
