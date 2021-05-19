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
    public class ProductoLogica
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public ProductoLogica()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static ProductoDTO ConvertirDatosProductoADTO(Producto producto)
        {
            return new ProductoDTO
            {
                SucursalAsociada = (producto.FkSucursalNavigation != null ?  SucursalLogica.ConvertirDatosSucursalADTO(producto.FkSucursalNavigation) : null),
                CantidadInventario = Convert.ToInt32(producto.CntProducto),
                IdEntidad = producto.PkProducto,
                NombreProducto = producto.NomProducto,
                PrecioUnitario = Convert.ToDecimal(producto.MtoPrecioUnitario),
                TipoProducto = Convert.ToInt32(producto.TipProducto)
            };
        }

        internal static Producto ConvertirDTOProductoADatos(ProductoDTO item)
        {
            return new Producto
            {
                CntProducto = item.CantidadInventario,
                TipProducto = item.TipoProducto,
                MtoPrecioUnitario = item.PrecioUnitario,
                NomProducto = item.NombreProducto
            };
        }

        #endregion

        #region Funcion


        public BaseDTO ObtenerProductoPorCodigo(int codigo)
        {
            try
            {
                ProductoDatos intermedioProducto = new ProductoDatos();

                var respuestaDatos = intermedioProducto.ObtenerProductoPorCodigo(codigo);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var solicitudCompraDTO = ConvertirDatosProductoADTO((Producto)respuestaDatos.ContenidoRespuesta);

                    return solicitudCompraDTO;
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


        public List<BaseDTO> FiltrarProductosPorParametros(string nombreProducto, int tipoProducto,
                                                           int sucursal, List<decimal> rangoPrecios)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {

                var intermedio = new ProductoDatos (contexto);
                var datosPrevios = new List<Producto>();

                if (nombreProducto != string.Empty || nombreProducto != null)
                {
                    datosPrevios = (List<Producto>)intermedio.FiltrarProductosPorParametros("Nombre", nombreProducto,
                                                                                            datosPrevios).ContenidoRespuesta;
                }
                if (tipoProducto != 0)
                {
                    datosPrevios = (List<Producto>)intermedio.FiltrarProductosPorParametros("Tipo", tipoProducto,
                                                                        datosPrevios).ContenidoRespuesta;
                }

                if (sucursal != 0)
                {
                    datosPrevios = (List<Producto>)intermedio.FiltrarProductosPorParametros("Sucursal", tipoProducto,
                                                    datosPrevios).ContenidoRespuesta;
                }

                if (rangoPrecios.Count > 0)
                {
                    datosPrevios = (List<Producto>)intermedio.FiltrarProductosPorParametros("Rango", rangoPrecios,
                                                    datosPrevios).ContenidoRespuesta;
                }

                if (datosPrevios.Count > 0)  // return data
                {
                    foreach (var item in datosPrevios)
                    {
                        respuesta.Add(ConvertirDatosProductoADTO(item));
                    }
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron resultados con los parametros establecidos");
                }
            }
            catch (Exception error)
            {

                respuesta.Clear();
                respuesta.Add(new ErrorDTO { MensajeError = error.Message });
                return respuesta;
            }
        }
        public BaseDTO AgregarProducto(ProductoDTO productoDTO)
        {
            try
            {
                var intermedia = new ProductoDatos(contexto);

                var productoDato = ConvertirDTOProductoADatos(productoDTO);

                var resultado = intermedia.AgregarProducto(productoDato);

                if (resultado.CodigoRespuesta != -1)
                {
                    // exito
                    return new BaseDTO
                    {
                        Mensaje = resultado.Mensaje + " Se actualizó un total de " + resultado.ContenidoRespuesta +
                                  " datos."
                    };
                }
                else
                {
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ActualizarCantidadPrecioProductoSucursal(int idProducto, int idSucursal,
                                                        int cantidad, decimal precio)
        {
            try
            {
                var intermedia = new ProductoDatos(contexto);

                var resultado = intermedia.ActualizarCantidadPrecioProductoSucursal(idProducto, idSucursal, cantidad, precio);

                if (resultado.CodigoRespuesta != -1)
                {
                    // exito
                    return new BaseDTO
                    {
                        Mensaje = resultado.Mensaje + " Se actualizó un total de " + resultado.ContenidoRespuesta +
                                  " datos."
                    };
                }
                else
                {
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        #endregion
        #endregion
    }
}
