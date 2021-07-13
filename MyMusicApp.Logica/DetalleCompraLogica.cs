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
    public class DetalleCompraLogica
    {
        #region Variables
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructores
        public DetalleCompraLogica()
        {

        }
        #endregion

        #region Metodos

        #region Conversiones
        internal static DetalleCompraDTO ConvertirDatosDetalleCompraADTO(DetalleCompra detalleCompra)
        {
            return new DetalleCompraDTO
            {
                ProductoAsociado = (detalleCompra.FkProductoNavigation != null ? ProductoLogica.ConvertirDatosProductoADTO(detalleCompra.FkProductoNavigation) : null),
                SolicitudCompraAsociada = (detalleCompra.FkOrdenCompraNavigation != null ? SolicitudCompraLogica.ConvertirDatosOrdenCompraADTO(detalleCompra.FkOrdenCompraNavigation) : null),
                CantidadProducto = detalleCompra.CntArticulo,
                Estado = detalleCompra.IndEstado

            };
        }

        internal static DetalleCompra ConvertirDTODetalleCompraADatos(DetalleCompraDTO item)
        {
            return new DetalleCompra
            {
                CntArticulo = item.CantidadProducto,
                IndEstado = item.Estado
            };
        }

        //internal static OrdenCompra ConvertirOrdenCompraDTOaDatos(SolicitudCompraDTO solicitudCompraDTO)
        //{
        //    return new OrdenCompra
        //    {
        //        FecOrden = solicitudCompraDTO.FechaOrden,
        //        PkOrdenCompra = solicitudCompraDTO.IdEntidad,
        //        IndEstado = solicitudCompraDTO.EstadoSolicitud,
        //        MntTotalOrden = solicitudCompraDTO.MontoTotal,
        //        TipEntrega = solicitudCompraDTO.TipoEntrega,
        //        MtoPctDescuento = solicitudCompraDTO.MtoPctDescuento
        //    };
        //}

        #endregion Conversiones

        #region Funciones


        public List<BaseDTO> ListasDetallesOrdenCompra(int codSolicitudCompra)
        {
            try
            {
                DetalleCompraDatos intermedioEjemplo = new DetalleCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListasDetallesOrdenCompra(codSolicitudCompra);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respDetalleCompra = new List<BaseDTO>();
                    foreach (var item in (List<DetalleCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respDetalleCompra.Add(ConvertirDatosDetalleCompraADTO(item));
                    }
                    return respDetalleCompra;
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

        public BaseDTO PrimerDetalleConProductoDeSegunda(int id)
        {
            try
            {
                DetalleCompraDatos intermedioEjemplo = new DetalleCompraDatos();
                RespuestaDTO prodDeSegunda = null;
                BaseDTO detalleCompraResp = null;
                var respuestaDatos = intermedioEjemplo.ListasDetallesOrdenCompra(id);
                bool found;

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    // List<BaseDTO> respDetalleCompra = new List<BaseDTO>();
                    found = false;
                    List<DetalleCompra> lst = (List<DetalleCompra>)respuestaDatos.ContenidoRespuesta;
                    for (int i = 0; i < lst.Count && !found; i++)
                    {
                        prodDeSegunda = new ProductoDatos().ObtenerProductoPorCodigo(lst[i].FkProducto);
                        found = (((Producto)prodDeSegunda.ContenidoRespuesta).IndSegunda == 1);
                        if (found)
                        {
                            detalleCompraResp = ConvertirDatosDetalleCompraADTO( lst[i]);
                        }
                    }

                    return detalleCompraResp;
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

        #endregion Funciones

        #endregion Metodos
    }
}
