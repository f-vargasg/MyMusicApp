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
                Producto = detalleCompra.FkProducto,
                CantidadProducto = detalleCompra.CntArticulo,
                Estado = detalleCompra.IndEstado

                //ClienteAsociado = (ordenCompra.FkClienteNavigation != null ? ClienteLogica.ConvertirDatosClienteADTO(ordenCompra.FkClienteNavigation) : null),
                //VendedorAsociado = (ordenCompra.FkVendedorNavigation != null ? VendedorLogica.ConvertirDatosVendedorADTO(ordenCompra.FkVendedorNavigation) : null),
                //FechaOrden = ordenCompra.FecOrden,
                //IdEntidad = ordenCompra.PkOrdenCompra,
                //MontoTotal = ordenCompra.MntTotalOrden,
                //TipoEntrega = ordenCompra.TipEntrega,
                //EstadoSolicitud = ordenCompra.IndEstado,
                //MtoPctDescuento = ordenCompra.MtoPctDescuento
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
        #endregion Funciones

        #endregion Metodos
    }
}
