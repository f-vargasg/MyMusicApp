using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class DetalleCompraDatos
    {
        #region Variables
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructor
        public DetalleCompraDatos(MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public DetalleCompraDatos()
        {

        }
        #endregion

        #region Método

        public RespuestaDTO ListasDetallesOrdenCompra(int codSolicitudCompra)
        {
            try
            {
                var detalleCompras = contexto.DetalleCompras.Where(D => D.FkOrdenCompra == codSolicitudCompra).ToList();

                if (detalleCompras.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = detalleCompras
                    };
                }
                else
                {
                    throw new Exception("No se encontraron los detalles de compras");
                }
            }
            catch (Exception error)
            {
                return new RespuestaDTO
                {
                    CodigoRespuesta = -1,
                    ContenidoRespuesta = new ErrorDTO { MensajeError = error.Message }
                };
            }
        }

        #endregion
    }
}
