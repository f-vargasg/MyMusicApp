using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class SolicitudCompraDatos
    {
        #region Variables
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructor
        public SolicitudCompraDatos(MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public SolicitudCompraDatos()
        {

        }
        #endregion

        #region Método


        /// <summary>
        /// 3.a Búsqueda de la solicitud de compra por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        public RespuestaDTO ObtenerSolicitudCompraPorCodigo(int codigo)
        {
            try
            {
                var solicitudCompra = contexto.OrdenCompras.FirstOrDefault(SC => SC.PkOrdenCompra == codigo);

                if (solicitudCompra != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontró la solicitud de compra por código en la base de datos [ObtenerSolicitudCompraPorCodigo]");
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
        /// 3.b. Listado de las solicitudes de compra según su Estado
        /// </summary>
        public RespuestaDTO ListarSolicitudesCompraPorEstado(int estado)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.IndEstado == estado).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por estado [ListarSolicitudesCompraPorEstado]");
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
        /// 3.c. Listado de las solicitudes de compra según el Cliente que las realizó
        /// </summary>
        public RespuestaDTO ListarSolicitudesCompraPorCliente(int cliente)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.FkCliente == cliente).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por cliente [ListarSolicitudesCompraPorCliente]");
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
        /// 3.d. Listado de las solicitudes de compra según el Vendedor que se les asignó
        /// </summary>
        public RespuestaDTO ListarSolicitudesCompraPorVendedor(int vendedor)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.FkVendedor == vendedor).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por vendedor [ListarSolicitudesCompraPorVendedor]");
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
        /// 3.e. Listado de las solicitudes de compra según su Tipo de entrega
        /// </summary>
        public RespuestaDTO ListarSolicitudesCompraPorTipoEntrega(int tipEntrega)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.TipEntrega == tipEntrega).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por tipo entrega [ListarSolicitudesCompraPorTipoEntrega]");
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
        /// 3.f. Listado total de solicitudes de compra
        /// </summary>
        /// <returns></returns>
        public RespuestaDTO ListarTotalSolicitudesCompra()
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = solicitudesCompra
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra [ListarTotalSolicitudesCompra]");
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
        /// 3.g Listado de solicitudes de compra filtradas por alguno o todos los siguientes parámetros: Estado, Tipo de Entrega y un rango de fechas (inicio y final) asociadas a la fecha de solicitud de compra.
        /// </summary>
        public RespuestaDTO FiltrarSolicitudesCompraPorParametros(string nombreParametro, object datoParametro, 
                                                                  List<OrdenCompra> datosPrevios)
        {
            try
            {
                List<OrdenCompra> respuesta = new List<OrdenCompra>();
                int valorInt = 0;
                List<DateTime> valoresFecha = new List<DateTime>();

                if (datosPrevios.Count() > 0)
                {
                    switch (nombreParametro)
                    {
                        case "IndEstado":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(SC => SC.IndEstado == valorInt).ToList();
                            break;
                        case "TipEntrega":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(SC => SC.TipEntrega == valorInt).ToList();
                            break;
                        case "FecOrden":
                            valoresFecha = (List<DateTime>)datoParametro;
                            datosPrevios = datosPrevios.Where(SC => SC.FecOrden >= valoresFecha.ElementAt(0) && SC.FecOrden <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = datoParametro
                    };
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "IndEstado":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.OrdenCompras.Where(SC => SC.IndEstado == valorInt).ToList();
                            break;
                        case "TipEntrega":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.OrdenCompras.Where(SC => SC.TipEntrega == valorInt).ToList();
                            break;
                        case "FecOrden":
                            valoresFecha = (List<DateTime>)datoParametro;
                            datosPrevios = contexto.OrdenCompras.Where(SC => SC.FecOrden >= valoresFecha.ElementAt(0) && SC.FecOrden <= valoresFecha.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = respuesta
                    };
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
        /// 3a. Registro de una solicitud de compra por primera vez, registrando todos los datos menos el vendedor
        /// </summary>
        /// <param name="fecOrden"></param>
        /// <param name="tipEntrega"></param>
        /// <param name="idCliente"></param>
        /// <param name="mtoTotalOrden"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO AgregarSolicitudCompra(DateTime fecOrden, int tipEntrega, int idCliente,  decimal mtoTotalOrden, 
                                                   int indEstado, decimal pctDescuento)
        {
            try
            {
                var ordenCompra = new OrdenCompra
                {
                    FecOrden = fecOrden, 
                    TipEntrega = tipEntrega,
                    FkCliente = idCliente,
                    MntTotalOrden = mtoTotalOrden,
                    IndEstado =indEstado,
                    MtoPctDescuento = pctDescuento
                   
                };
                contexto.OrdenCompras.Add(ordenCompra);

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
                    throw new Exception("No se pudo guardar la orden de compra, por favor revisar los datos suministrados");
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
        /// 3b. Registro de solicitud de compra por primera vez, registrando todos los datos incluido el vendedor
        /// </summary>
        /// <param name="fecOrden"></param>
        /// <param name="tipEntrega"></param>
        /// <param name="idCliente"></param>
        /// <param name="mtoTotalOrden"></param>
        /// <param name="idVendedor"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO AgregarSolicitudCompra(DateTime fecOrden, int tipEntrega, int idCliente, decimal mtoTotalOrden,
                                                   int idVendedor,   int indEstado, decimal pctDescuento)
        {
            try
            {
                var ordenCompra = new OrdenCompra
                {
                    FecOrden = fecOrden,
                    TipEntrega = tipEntrega,
                    FkCliente = idCliente,
                    MntTotalOrden = mtoTotalOrden,
                    FkVendedor = idVendedor,
                    IndEstado = indEstado,
                    MtoPctDescuento = pctDescuento
                };
                contexto.OrdenCompras.Add(ordenCompra);

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
                    throw new Exception("No se pudo guardar la orden de compra, por favor revisar los datos suministrados");
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
        /// 3.c Actualización del Vendedor asignado a la solicitud de compra
        /// </summary>
        /// <param name="idOrdenCompra"></param>
        /// <param name="idVendedor"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarVendedorOrdenCompra(int idOrdenCompra, int idVendedor)
        {
            try
            {
                var ordenCompra = contexto.OrdenCompras.FirstOrDefault(S => S.PkOrdenCompra == idOrdenCompra);
                if (ordenCompra != null)
                {
                    // Hacemos la actualizacion
                    ordenCompra.FkVendedor = idVendedor;


                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = ordenCompra
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el vendedor de la orden compra, por favor revise los datos suministrados");
                    }

                }
                else
                {
                    throw new Exception("No se encontró la orden de compra con el código suministrado");
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
        /// 3d. Actualización del Estado de la orden compra
        /// </summary>
        /// <param name="idOrdenCompra"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarEstadoOrdenCompra(int idOrdenCompra, int indEstado)
        {
            try
            {
                var ordenCompra = contexto.OrdenCompras.FirstOrDefault(S => S.PkOrdenCompra == idOrdenCompra);
                if (ordenCompra != null)
                {
                    // Hacemos la actualizacion
                    ordenCompra.IndEstado = indEstado;


                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = ordenCompra
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el estado de la orden compra, por favor revise los datos suministrados");
                    }

                }
                else
                {
                    throw new Exception("No se encontró la orden de compra con el código suministrado");
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
