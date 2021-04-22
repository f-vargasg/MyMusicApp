using MyMusicApp.Datos.MyMusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class SolicitudEnvioDomicDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public SolicitudEnvioDomicDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        public SolicitudEnvioDomicDatos()
        {

        }
        #endregion

        #region Metodos
        public object ObtenerSolicitudEnvioPorPk(int idSolEnvio)
        {
            try
            {
                //                      1      2        3               4                    3*       
                // var producto = contexto.Productos.Where(P => P.PkProducto == codigo).FirstOrDefault();

                //                      1      2        3               4                    3*       
                var solicitudEnvio = contexto.SolicitudEnvioDomics.FirstOrDefault(P => P.PkSolicitudEnvio == idSolEnvio);
                if (solicitudEnvio != null)
                {
                    return solicitudEnvio;
                }
                else
                {
                    throw new Exception("No se encontró Solicitud de Envio  con la id Solicitud suministrado");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public object ListarSolicitudesPorEstado(int indEstado)
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

        public object ListadoTotalSolicitudesEnvio()
        {
            try
            {
                //                      1      2        3               4                    3*       
                // var producto = contexto.Productos.Where(P => P.PkProducto == codigo).FirstOrDefault();

                //                      1      2        3               4                    3*       
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


        public object ListarSolicitudesEnvioPorEstadoRngFecEnvioRngFecRecibo (string nombreParametro, object datoParemtro, 
                                                                                List<SolicitudEnvioDomic> datosPrevios)
        {
            try
            {
                List<SolicitudEnvioDomic> respuesta = new List<SolicitudEnvioDomic>();
                int valorInt = 0;
                // string valorString = string.Empty;
                List<DateTime> valoresFecha = new List<DateTime>();

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Estado":
                            valorInt = Convert.ToInt32(datoParemtro);
                            datosPrevios = datosPrevios.Where(S => S.IndEstado == valorInt).ToList();
                            break;
                        case "RangoFecEnvio":
                            valoresFecha = (List<DateTime>)datoParemtro;
                            datosPrevios = datosPrevios.Where(P => P.FecEnvio >= valoresFecha.ElementAt(0)
                                                                      && P.FecEnvio <= valoresFecha.ElementAt(1)).ToList(); 
                            break;
                        case "RangoFecRecibo":
                            valoresFecha = (List<DateTime>)datoParemtro;
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
                            valoresFecha = (List<DateTime>)datoParemtro;
                            respuesta = contexto.SolicitudEnvioDomics.Where(S => S.FecEnvio >= valoresFecha.ElementAt(0)
                                                                      && S.FecEnvio <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        case "RangoFecRecibo":
                            valoresFecha = (List<DateTime>)datoParemtro;
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
