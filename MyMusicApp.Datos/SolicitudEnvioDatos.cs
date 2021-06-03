using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class SolicitudEnvioDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public SolicitudEnvioDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        public SolicitudEnvioDatos()
        {

        }
        #endregion

        #region Metodos

        /// <summary>
        /// 5.a. Búsqueda de la solicitud de envío por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerSolicitudEnvioPorCodigo(int codigo)
        {
            try
            {
                var solicitudEnvio = contexto.SolicitudEnvioDomics.FirstOrDefault(P => P.PkSolicitudEnvio == codigo);
                if (solicitudEnvio != null)
                {
                    return solicitudEnvio;
                }
                else
                {
                    throw new Exception("No se encontró Solicitud de Envio con el codigo suministrado");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 5.b. Listado de las solicitudes de envío según su Estado
        /// </summary>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO ListarSolicitudesEnvioPorEstado(int indEstado)
        {
            try
            {
                var solicitudesEnvio = contexto.SolicitudEnvioDomics.Where(S => S.IndEstado == indEstado).ToList();

                if (solicitudesEnvio.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesEnvio
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de envio por estado [ListarSolicitudesCompraPorEstado]");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException == null)
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

        /// <summary>
        /// 5.c. Listado total de solicitudes de envío.
        /// </summary>
        /// <returns></returns>
        public RespuestaDTO ListarTotalSolicitudesEnvio()
        {
            try
            {
                var solicitudesEnvio = contexto.SolicitudEnvioDomics.ToList();

                if (solicitudesEnvio.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesEnvio
                    };
                }
                else
                {
                    throw new Exception("No se encontraron solicitudes de envio ");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException == null)
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

        /// <summary>
        /// 5.d. Listado de solicitudes de envío filtradas por alguno o todos los siguientes parámetros: Estado, un rango de fechas (inicio y final) asociadas a la fecha de envío y un rango de fechas (inicio y final) asociadas a la fecha de recibido.
        /// </summary>
        /// <param name="nombreParametro"></param>
        /// <param name="datoParametro"></param>
        /// <param name="datosPrevios"></param>
        /// <returns></returns>
        public object FiltrarSolicitudesEnvioPorParametros(string nombreParametro, object datoParametro, List<SolicitudEnvioDomic> datosPrevios)
        {
            try
            {
                List<SolicitudEnvioDomic> respuesta = new List<SolicitudEnvioDomic>();
                int valorInt = 0;
                List<DateTime> valoresFecha = new List<DateTime>();

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Estado":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(S => S.IndEstado == valorInt).ToList();
                            break;
                        case "RangoFecEnvio":
                            valoresFecha = (List<DateTime>)datoParametro;
                            datosPrevios = datosPrevios.Where(P => P.FecEnvio >= valoresFecha.ElementAt(0)
                                                                      && P.FecEnvio <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        case "RangoFecRecibo":
                            valoresFecha = (List<DateTime>)datoParametro;
                            datosPrevios = datosPrevios.Where(P => P.FecRecibo >= valoresFecha.ElementAt(0)
                                                                      && P.FecRecibo <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                    return datosPrevios;
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "Estado":
                            respuesta = contexto.SolicitudEnvioDomics.Where(S => S.IndEstado == valorInt).ToList();
                            break;
                        case "RangoFecEnvio":
                            valoresFecha = (List<DateTime>)datoParametro;
                            respuesta = contexto.SolicitudEnvioDomics.Where(S => S.FecEnvio >= valoresFecha.ElementAt(0)
                                                                      && S.FecEnvio <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        case "RangoFecRecibo":
                            valoresFecha = (List<DateTime>)datoParametro;
                            respuesta = contexto.SolicitudEnvioDomics.Where(S => S.FecRecibo >= valoresFecha.ElementAt(0)
                                                                      && S.FecRecibo <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                }
                return respuesta;
            }
            catch (Exception error)
            {
                return error.Message;
                throw;
            }
        }

        /// <summary>
        /// 6a. Registro de una solicitud de envio por primera vez (insert)
        /// </summary>
        /// <param name="desUbicacion"></param>
        /// <param name="fecEnvio"></param>
        /// <param name="fecRecibo"></param>
        /// <param name="idOrdenCompra"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO AgregarSolicitudEnvio(string desUbicacion, DateTime fecEnvio, DateTime fecRecibo,
                                                  int idOrdenCompra, int indEstado)
        {
            try
            {
                var solicitudEnvio = new SolicitudEnvioDomic
                {
                    DesUbicEnvio = desUbicacion,
                    FecEnvio = fecEnvio,
                    FecRecibo = fecRecibo,
                    FkOrdenCompra = idOrdenCompra,
                    IndEstado = indEstado
                };
                contexto.SolicitudEnvioDomics.Add(solicitudEnvio);

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
                    throw new Exception("No se pudo guardar la solicitud de envio, por favor revisar los datos suministrados");
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


        /// <summary>
        /// 6b. Actualización del estado de la solicitud de Envio
        /// </summary>
        /// <param name="idSolEnvio"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarEstadoSolicitudEnvio(int idSolEnvio, int indEstado)
        {
            try
            {
                var solicitudEnvio = contexto.SolicitudEnvioDomics.FirstOrDefault(S => S.PkSolicitudEnvio == idSolEnvio);
                if (solicitudEnvio != null)
                {
                    // Hacemos la actualizacion
                    solicitudEnvio.IndEstado = indEstado;


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
                        throw new Exception("No se pudo actualizar el estado de la solcitud de envio, por favor revise los datos suministrados");
                    }

                }
                else
                {
                    throw new Exception("No se encontró la solicitud con el código suministrado");
                }
                // return true;
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


        /// <summary>
        /// 6c. Actualización de la fecha de envío de la solicitud
        /// </summary>
        /// <param name="idSolEnvio"></param>
        /// <param name="fecEnvio"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarFechaEnvioSolicitudEnvio(int idSolEnvio, DateTime fecEnvio)
        {
            try
            {
                var solicitudEnvio = contexto.SolicitudEnvioDomics.FirstOrDefault(S => S.PkSolicitudEnvio == idSolEnvio);
                if (solicitudEnvio != null)
                {
                    // Hacemos la actualizacion
                    solicitudEnvio.FecEnvio = fecEnvio;


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
                        throw new Exception("No se pudo actualizar la fecha de envio de la solcitud de envio, por favor revise los datos suministrados");
                    }

                }
                else
                {
                    throw new Exception("No se encontró la solicitud con el código suministrado");
                }
                // return true;
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


        /// <summary>
        /// 6d. Actualización de la fecha de recibido de la solicitud (se puede combinar con el método del punto 6.c. 
        /// siempre ambas fechas sean opcionales, es decir se puede enviar una u otra; validando que, si no viene 
        /// ninguna de las dos, se lance un error).
        /// </summary>
        /// <param name="idSolEnvio"></param>
        /// <param name="fecRecibo"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarFechaReciboSolicitudEnvio(int idSolEnvio, DateTime fecRecibo)
        {
            try
            {
                var solicitudEnvio = contexto.SolicitudEnvioDomics.FirstOrDefault(S => S.PkSolicitudEnvio == idSolEnvio);
                if (solicitudEnvio != null)
                {
                    // Hacemos la actualizacion
                    solicitudEnvio.FecRecibo = fecRecibo;


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
                        throw new Exception("No se pudo actualizar la fecha de recibo de la solcitud de envio, por favor revise los datos suministrados");
                    }

                }
                else
                {
                    throw new Exception("No se encontró la solicitud con el código suministrado");
                }
                // return true;
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



        #endregion
    }
}
