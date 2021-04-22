using MyMusicApp.Datos.MyMusicModel;
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
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public SolicitudCompraDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
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
        public object ObtenerSolicitudCompraPorCodigo(int codigo)
        {
            try
            {
                var solicitudCompra = contexto.OrdenCompras.FirstOrDefault(SC => SC.PkOrdenCompra == codigo);

                if (solicitudCompra != null)
                {
                    return solicitudCompra;
                }
                else
                {
                    throw new Exception("No se encontró la solicitud de compra por código en la base de datos [ObtenerSolicitudCompraPorCodigo]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.b. Listado de las solicitudes de compra según su Estado
        /// </summary>
        public object ListarSolicitudesCompraPorEstado(int estado)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.IndEstado == estado).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return solicitudesCompra;
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por estado [ListarSolicitudesCompraPorEstado]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.c. Listado de las solicitudes de compra según el Cliente que las realizó
        /// </summary>
        public object ListarSolicitudesCompraPorCliente(int cliente)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.FkCliente == cliente).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return solicitudesCompra;
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por cliente [ListarSolicitudesCompraPorCliente]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.d. Listado de las solicitudes de compra según el Vendedor que se les asignó
        /// </summary>
        public object ListarSolicitudesCompraPorVendedor(int vendedor)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.FkVendedor == vendedor).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return solicitudesCompra;
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por vendedor [ListarSolicitudesCompraPorVendedor]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.e. Listado de las solicitudes de compra según su Tipo de entrega
        /// </summary>
        public object ListarSolicitudesCompraPorTipoEntrega(int tipEntrega)
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.Where(SC => SC.TipEntrega == tipEntrega).ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return solicitudesCompra;
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra por tipo entrega [ListarSolicitudesCompraPorTipoEntrega]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.f. Listado total de solicitudes de compra
        /// </summary>
        /// <returns></returns>
        public object ListarTotalSolicitudesCompra()
        {
            try
            {
                var solicitudesCompra = contexto.OrdenCompras.ToList();

                if (solicitudesCompra.Count > 0)
                {
                    return solicitudesCompra;
                }
                else
                {
                    throw new Exception("No se encontraron las solicitudes de compra [ListarTotalSolicitudesCompra]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.g Listado de solicitudes de compra filtradas por alguno o todos los siguientes parámetros: Estado, Tipo de Entrega y un rango de fechas (inicio y final) asociadas a la fecha de solicitud de compra.
        /// </summary>
        public object FiltrarSolicitudesCompraPorParametros(string nombreParametro, object datoParametro, List<OrdenCompra> datosPrevios)
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
                    return datosPrevios;
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
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        #endregion
    }
}
