using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Logica
{
    public class DetaSolCompra
    {
        #region Variables
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructores
        public DetaSolCompra()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static DetalleSolCompraDTO ConvertirDatosSolCompraADTO(DetalleCompra detalleCompra)
        {
            return new DetalleSolCompraDTO
            {
                ProductoAsociado = ProductoLogica.ConvertirDatosProductoADTO(detalleCompra.FkProductoNavigation),
                SolicitudCompraAsociada = SolicitudCompraLogica.ConvertirDatosOrdenCompraADTO(detalleCompra.FkOrdenCompraNavigation),
                CantProducto = detalleCompra.CntArticulo,
                IdEntidad = detalleCompra.PkDetalleOrden,
                IndEstado = detalleCompra.IndEstado,
            };
        }

        internal static DetalleCompra ConvertirDetalleSolCompraDTOaDatos(DetalleSolCompraDTO detalleSolCompraDTO)
        {
            return new DetalleCompra
            {
                CntArticulo = detalleSolCompraDTO.CantProducto,
                IndEstado = detalleSolCompraDTO.IndEstado,
            };
        }
        #endregion
        #endregion
    }
}
