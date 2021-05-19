using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class ProductoDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public ProductoDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public ProductoDatos()
        {

        }
        #endregion

        #region Método




        // Laboratorio #1 - 5.a. Búsqueda de productos según su Primary Key
        public RespuestaDTO ObtenerProductoPorCodigo(int codigo)
        {
            try
            {
                var producto = contexto.Productos.FirstOrDefault(P => P.PkProducto == codigo);

                if (producto != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = producto
                    };
                }
                else
                {
                    throw new Exception("No se encontró la sucursal por código en la base de datos [ObtenerSucursalPorCodigo]");
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


        // Laboratorio #1 - 5.b. Búsqueda de productos filtrados por alguno o todos los siguientes parámetros: nombre del producto, tipo, sucursal y rango de precios.
        public RespuestaDTO FiltrarProductosPorParametros(string nombreParametro, object datoParametro, List<Producto> datosPrevios)
        {
            try
            {
                List<Producto> respuesta = new List<Producto>();
                int valorInt = 0;
                List<Decimal> valoresPrecio = new List<decimal>();

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Nombre":
                            datosPrevios = datosPrevios.Where(P => P.NomProducto.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(P => P.TipProducto == valorInt).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(P => P.FkSucursal == valorInt).ToList();
                            break;
                        case "RangoPrecios":
                            valoresPrecio = (List<Decimal>)datoParametro;
                            datosPrevios = datosPrevios.Where(P => P.MtoPrecioUnitario >= valoresPrecio.ElementAt(0) && P.MtoPrecioUnitario <= valoresPrecio.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = datosPrevios
                    };
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "Nombre":
                            respuesta = contexto.Productos.Where(P => P.NomProducto.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.Productos.Where(P => P.TipProducto == valorInt).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.Productos.Where(P => P.FkSucursal == valorInt).ToList();
                            break;
                        case "RangoPrecios":
                            valoresPrecio = (List<Decimal>)datoParametro;
                            respuesta = contexto.Productos.Where(P => P.MtoPrecioUnitario >= valoresPrecio.ElementAt(0) && P.MtoPrecioUnitario <= valoresPrecio.ElementAt(1)).ToList();
                            break;
                        default:
                            break;
                    }
                }
                return new RespuestaDTO
                {
                    CodigoRespuesta = 1,
                    ContenidoRespuesta = respuesta
                };
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


        // Laboratorio #1 - 5.c. Registro de un producto por primera vez (insert)
        public RespuestaDTO AgregarProducto(Producto producto)
        {
            try
            {
                contexto.Productos.Add(producto);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = producto
                    };
                }
                else
                {
                    throw new Exception("No se pudo insertar el producto");
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


        // Laboratorio #1 - 5.d. Actualización de la cantidad en inventario y el precio de un producto específico, según la sucursal asociada
        public RespuestaDTO ActualizarCantidadPrecioProductoSucursal(int idProducto, int idSucursal, int cantidad, decimal precio)
        {
            try
            {
                var producto = contexto.Productos.FirstOrDefault(P => P.PkProducto == idProducto && P.FkSucursal == idSucursal);

                if (producto != null)
                {
                    producto.CntProducto = cantidad;
                    producto.MtoPrecioUnitario = precio;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = producto
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar la cantidad y el precio del producto según sucursal");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el producto según sucursal especificada");
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


        #endregion
    }
}
