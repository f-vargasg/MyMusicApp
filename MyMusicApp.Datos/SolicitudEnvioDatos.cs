using MyMusicApp.Datos.MyMusicModel;
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
        public object ListarSolicitudesEnvioPorEstado(int indEstado)
        {
            try
            {
                var solicitudesEnvio = contexto.SolicitudEnvioDomics.Where(S => S.IndEstado == indEstado).ToList();

                if (solicitudesEnvio.Count > 0)
                {
                    return solicitudesEnvio;
                }
                else
                {
                    return new Exception("No existe solicitudes de envio por el estado indicado");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 5.c. Listado total de solicitudes de envío.
        /// </summary>
        /// <returns></returns>
        public object ListarTotalSolicitudesEnvio()
        {
            try
            {
                var solicitudesEnvio = contexto.SolicitudEnvioDomics.ToList();

                if (solicitudesEnvio.Count > 0)
                {
                    return solicitudesEnvio;
                }
                else
                {
                    throw new Exception("No se encontraron solicitudes de envio ");
                }
            }
            catch (Exception error)
            {
                return error.Message;
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

        #endregion
    }
}
