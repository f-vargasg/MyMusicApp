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
    public class SolicitudEnvioLogica
    {
        #region Variables
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructores
        public SolicitudEnvioLogica()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static SolicitudEnvioDTO ConvertirDatosSolicitudEnvioADTO(SolicitudEnvio solicitudEnvio)
        {
            return new SolicitudEnvioDTO
            {
                // OrdenCompraAsociada = (solicitudEnvio.FkOrdenCompraNavigation != null ? SolicitudCompraLogica.ConvertirDatosOrdenCompraADTO(solicitudEnvio.FkOrdenCompraNavigation) : null),
                OrdenCompraAsociada = new SolicitudCompraDTO { IdEntidad = solicitudEnvio.FkOrdenCompra },
                EstadoSolicEnvio = solicitudEnvio.IndEstado,
                FecEnvio = solicitudEnvio.FecEnvio,
                FechaRecibido = solicitudEnvio.FecRecibo,
                UbicacionEnvio = solicitudEnvio.DesUbicEnvio,
                IdEntidad = solicitudEnvio.PkSolicitudEnvio,
                MtoPctComision = solicitudEnvio.MtoPctComision
            };
        }

        internal static SolicitudEnvio ConvertirSolicitudEnvioDTOaDatos(SolicitudEnvioDTO solicitudEnvioDTO)
        {
            return new SolicitudEnvio
            {
                DesUbicEnvio = solicitudEnvioDTO.UbicacionEnvio,
                FecEnvio = solicitudEnvioDTO.FecEnvio,
                FecRecibo = solicitudEnvioDTO.FechaRecibido,
                IndEstado = solicitudEnvioDTO.EstadoSolicEnvio,
                MtoPctComision = solicitudEnvioDTO.MtoPctComision
            };
        }
        #endregion

        #region Funcion

        public BaseDTO ObtenerSolicitudEnvioPorCodigo(int codigo)
        {
            try
            {
                SolicitudEnvioDatos intermedioEjemplo = new SolicitudEnvioDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerSolicitudEnvioPorCodigo(codigo);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var solicitudEnvioDTO = ConvertirDatosSolicitudEnvioADTO((SolicitudEnvio)respuestaDatos.ContenidoRespuesta);

                    return solicitudEnvioDTO;
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
        public BaseDTO AgregarSolicitudEnvio(string desUbicacion, DateTime fecEnvio, DateTime fecRecibo,
                                                  int idOrdenCompra, int indEstado, decimal mtoPctComision)
        {
            try
            {
                var solicitudEnvio = new SolicitudEnvio
                {
                    DesUbicEnvio = desUbicacion,
                    FecEnvio = fecEnvio,
                    FecRecibo = fecRecibo,
                    FkOrdenCompra = idOrdenCompra,
                    IndEstado = indEstado,
                    MtoPctComision = mtoPctComision
                };
                contexto.SolicitudEnvios.Add(solicitudEnvio);

                var guardado = contexto.SaveChanges();

                if (guardado > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = guardado,
                        Mensaje = "Los datos se guardaron correctamente"
                    };
                }
                else
                {
                    throw new Exception("No se pudo guardar la Solicitud de Envio, por favor revisar los datos suministrados");
                }

                // return true;
            }
            catch (Exception error)
            {
                if (error.Message.Contains("ERROR controlado"))
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.Message }
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = "ERROR NO CONTROLADO" + error.InnerException }
                    };
                }
            }
        }

        public BaseDTO ActualizarEstadoSolicitudEnvio(int idSolEnvio, int indEstado)
        {
            try
            {
                var intermedia = new SolicitudEnvioDatos(contexto);
                var resultado = intermedia.ActualizarEstadoSolicitudEnvio(idSolEnvio, indEstado);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó el estado de la solicitud " + Convert.ToString(indEstado)
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
        public BaseDTO ActualizarFechaEnvioSolicitudEnvio(int idSolEnvio, DateTime fecEnvio)
        {
            try
            {
                var intermedia = new SolicitudEnvioDatos(contexto);
                var resultado = intermedia.ActualizarFechaEnvioSolicitudEnvio(idSolEnvio, fecEnvio);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó la fecha de envio de la solicitud a " +
                                   fecEnvio.ToString("dd/MM/yyyy")
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

        public BaseDTO ActualizarFechaReciboSolicitudEnvio(int idSolEnvio, DateTime fecRecibo)
        {
            try
            {
                var intermedia = new SolicitudEnvioDatos(contexto);
                var resultado = intermedia.ActualizarFechaEnvioSolicitudEnvio(idSolEnvio, fecRecibo);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó la fecha de recibo de la solicitud a " +
                                   fecRecibo.ToString("dd/MM/yyyy")
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

        public RespuestaDTO ActualizarPctComisionProductosSegunda(int idEnvio, decimal pctComision)
        {
            try
            {
                var solicitudEnvio = contexto.SolicitudEnvios.FirstOrDefault(S => S.PkSolicitudEnvio == idEnvio);

                if (solicitudEnvio != null)
                {
                    solicitudEnvio.MtoPctComision = pctComision;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = solicitudEnvio
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el porcentaje de comisión de la solicitud de envío");
                    }
                }
                else
                {
                    throw new Exception("No se encontró la solicitud de envío.");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.Message }
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.InnerException.Message }
                    };
                }
            }
        }

        public List<BaseDTO> ListarSolicitudesEnvio()
        {
            try
            {
                SolicitudEnvioDatos intermedioEjemplo = new SolicitudEnvioDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesEnvio();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitudEnvio = new List<BaseDTO>();
                    foreach (var item in (List<SolicitudEnvio>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitudEnvio.Add(ConvertirDatosSolicitudEnvioADTO(item));
                    }
                    return respSolicitudEnvio;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListarSolicitudesEnvioPorEstado(int indEstado)
        {
            try
            {
                SolicitudEnvioDatos intermedioEjemplo = new SolicitudEnvioDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesEnvioPorEstado(indEstado);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitudEnvio = new List<BaseDTO>();
                    foreach (var item in (List<SolicitudEnvio>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitudEnvio.Add(ConvertirDatosSolicitudEnvioADTO(item));
                    }
                    return respSolicitudEnvio;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }
        
        #endregion
        #endregion
    }
}
