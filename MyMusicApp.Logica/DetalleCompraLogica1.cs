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

        public List<BaseDTO> ListarDetalleSolCompraPorIdCompra(int idOrdenCompra)
        {
            try
            {
                DetalleCompraDatos intermedioEjemplo = new DetalleCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListasDetallesOrdenCompra(idOrdenCompra);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDetalleSolCompraDTOaDatos(item));
                    }
                    return respSolicitCompra;
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

        public bool TieneProductosSegunda(int id)
        {
            var listDetalleCompra = new DetalleCompraLogica().ListasDetallesOrdenCompra(model.SolicitudEnvio.OrdenCompraAsociada.IdEntidad);
            foreach (var item in listDetalleCompra)
            {
                var producto = new ProductoLogica().ObtenerProductoPorCodigo(((DetalleCompraDTO)item).Producto);

                if (((ProductoDTO)producto).IndSegunda == 1)
                {
                    model.SolicitudEnvio.MtoPctComision = Convert.ToDecimal(15.0 / 100.0);
                }
            }
        }

        #endregion
    }
}
