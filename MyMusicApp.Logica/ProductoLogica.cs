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
        MusicStoreDBContext contexto = new MusicStoreDBContext();
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
                SucursalAsociada = (producto.FkSucursalNavigation != null ? SucursalLogica.ConvertirDatosSucursalADTO(producto.FkSucursalNavigation) : null),
                CantidadInventario = Convert.ToInt32(producto.CntProducto),
                IdEntidad = producto.PkProducto,
                NombreProducto = producto.NomProducto,
                PrecioUnitario = Convert.ToDecimal(producto.MtoPrecioUnitario),
                TipoProducto = Convert.ToInt32(producto.TipProducto),
                IndSegunda = Convert.ToInt32(producto.IndSegunda)
            };
        }

        internal static Producto ConvertirDTOProductoADatos(ProductoDTO item)
        {
            return new Producto
            {
                CntProducto = item.CantidadInventario,
                TipProducto = item.TipoProducto,
                MtoPrecioUnitario = item.PrecioUnitario,
                NomProducto = item.NombreProducto,
                IndSegunda = item.IndSegunda
            };
        }

        #endregion

        #region Funcion

        public List<BaseDTO> ListarProductos()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();

            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ListarProductos();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var lista = ((List<Producto>)respuestaDatos.ContenidoRespuesta);
                    for (int i = 0; i < lista.Count; i++)
                    {
                        respuesta.Add(ConvertirDatosProductoADTO(lista[i]));
                    }
                    return respuesta;
                }
                else
                {
                    respuesta.Clear();
                    respuesta.Add((ErrorDTO)respuestaDatos.ContenidoRespuesta);
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta.Clear();
                respuesta.Add(new ErrorDTO { MensajeError = error.Message });
                return respuesta;
            }
        }

        public List<BaseDTO> ListarProductosDeSegunda()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();

            try
            {
                // ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                ProductoDatos intermedioDatos = new ProductoDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ListarProductosDeSegunda();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var lista = ((List<Producto>)respuestaDatos.ContenidoRespuesta);
                    for (int i = 0; i < lista.Count; i++)
                    {
                        respuesta.Add(ConvertirDatosProductoADTO(lista[i]));
                    }
                    return respuesta;
                }
                else
                {
                    respuesta.Clear();
                    respuesta.Add((ErrorDTO)respuestaDatos.ContenidoRespuesta);
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta.Clear();
                respuesta.Add(new ErrorDTO { MensajeError = error.Message });
                return respuesta;
            }
        }

        public BaseDTO ListarProductoDeSegundaParecidosA(string nomProducto)
        {
            try
            {
                ProductoDatos intermedioProducto = new ProductoDatos();

                var respuestaDatos = intermedioProducto.ObtenerProductoDeSegundaParecidosA(nomProducto);
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
        public BaseDTO ObtenerProductoPorCodigo(int codigo)
        {
            try
            {
                ProductoDatos intermedioProducto = new ProductoDatos();

                var respuestaDatos = intermedioProducto.ObtenerProductoPorCodigo(codigo);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var productoDTO = ConvertirDatosProductoADTO((Producto)respuestaDatos.ContenidoRespuesta);

                    return productoDTO;
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
