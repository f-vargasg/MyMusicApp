using System;
using System.Collections.Generic;
using MyMusicApp.Datos;
using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;

namespace MyMusicApp.Logica
{
    public class ClaseEjemploLogica
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public ClaseEjemploLogica()
        {

        }
        #endregion


        #region Metodos
        #region Conversiones 
        // Entidad Convertida                   // entidad a convertir    
        internal static SucursalDTO ConvertirDatosSucursalADTO(Sucursal sucursal)
        {
            return new SucursalDTO
            {
                CorreoElectronico = sucursal.EmlSucursal,
                DirSucursal = sucursal.DirUbicacion,
                HorarioSucursal = sucursal.DesHorario,
                IdEntidad = sucursal.PkSucursal,
                TelefonoSucursal = sucursal.TelSucursal
            };
        }

        internal static Sucursal ConvertirSucursalDTOaDatos(SucursalDTO sucursalDTO)
        {
            return new Sucursal
            {
                DesHorario = sucursalDTO.HorarioSucursal,
                DirUbicacion = sucursalDTO.DirSucursal,
                EmlSucursal = sucursalDTO.CorreoElectronico,
                TelSucursal = sucursalDTO.TelefonoSucursal
            };
        }

        internal static ProductoDTO ConvertirDatosProductoADTO(Producto producto)
        {
            return new ProductoDTO
            {
                SucursalAsociada = (producto.FkSucursalNavigation != null ? ConvertirDatosSucursalADTO(producto.FkSucursalNavigation) : null),
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

        internal static VendedorDTO ConvertirDatosVendedorADTO(Vendedor vendedor)
        {
            return new VendedorDTO
            {
                ClaveVendedor = vendedor.UsrPassword,
                IdEntidad = vendedor.PkVendedor,
                NombreVendedor = vendedor.NomVendedor,
                Puesto = vendedor.DesPuesto,
                UsuarioVendedor = vendedor.UsrVendedor
            }; 
        }

        

        #endregion

        #region Funciones
        public BaseDTO ObtenerSucursalPorCodigo (int codigoSucursal)
        {
            try
            {
                // ClaseEjemploDatos intermedioEjemplo = new ClaseEjemploDatos(this.contexto);  // Este se puede usar tambien
                ClaseEjemploDatos intermedioEjemplo = new ClaseEjemploDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerProductoPorCodigo(codigoSucursal);
                // Caso Exitoso
                if (respuestaDatos.CodigoRespuesta == 1) // ok
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);

                    return sucursalRespuesta;
                }
                else 
                {
                    // excepcion controlada
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }

            }
            catch (Exception error )
            {
                // excepciones no controladas
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerSucursalPorCodigo(SucursalDTO sucursalDTO)
        {
            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ObtenerProductoPorCodigo(sucursalDTO.IdEntidad);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var sucursalRespuesta = ConvertirDatosSucursalADTO((Sucursal)respuestaDatos.ContenidoRespuesta);
                    return sucursalRespuesta;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }

        }


        public List<BaseDTO> ObtenerProductoPorTipo(int tipoProducto)
        {
            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);
                var respuestaDatos = intermedioDatos.ListarProductosPorTipo(tipoProducto);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respuestaProductos = new List<BaseDTO>();
                    // llamada exitosa
                    foreach (var item in (List<Producto>)respuestaDatos.ContenidoRespuesta)
                    {
                        respuestaProductos.Add(ConvertirDatosProductoADTO(item));
                    }
                    return respuestaProductos;
                }
                else
                {
                    // Error controlado
                    // return new List<BaseDTO> { (ErrorDTO)respuestaDatos.ContenidoRespuesta };     // opcion 1
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new  ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListaProductosPorTipo(ProductoDTO productoDTO)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();

            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ListarProductosPorTipo(productoDTO.TipoProducto);
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

        public List<List<BaseDTO>> ListarVendedoresDeSucursales()
        {
            List<List<BaseDTO>> respuesta = new List<List<BaseDTO>>();
            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.ListarVendedorDeSucursales();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    foreach (var item in  (List<Sucursal>)respuestaDatos.ContenidoRespuesta)
                    {
                        respuesta.Add(new List<BaseDTO> { ConvertirDatosSucursalADTO(item) });

                        List<BaseDTO> vendedoresSucursal = new List<BaseDTO>();

                        foreach (var itemVendedor in item.Vendedors)
                        {
                            vendedoresSucursal.Add(ConvertirDatosVendedorADTO(itemVendedor));
                        }
                        respuesta.Add(vendedoresSucursal);
                    }
                }
                else  // error controlado
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
                }
                return respuesta;
            }
            catch (Exception error)
            {
                respuesta.Clear();
                respuesta.Add(new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } });
                return respuesta;
            }
        }

        public BaseDTO CalcularVentasAcumuladas()
        {
            try
            {
                ClaseEjemploDatos intermedioDatos = new ClaseEjemploDatos(this.contexto);

                var respuestaDatos = intermedioDatos.CalcularVentasAcum();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    return new BaseDTO
                    {
                        Mensaje = respuestaDatos.GetType().Name+ " - " + respuestaDatos.ContenidoRespuesta.ToString()
                    };
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO AgregarSucursalConProducto(SucursalDTO sucursal, List<ProductoDTO> productos)
        {
            try
            {
                var intermedia = new ClaseEjemploDatos(contexto);

                var sucursalDato = ConvertirSucursalDTOaDatos(sucursal);

                List<Producto> listadoProductosDatos = new List<Producto>();
                foreach (var item in productos)
                {
                    // primer opcion
                    //var temp = ConvertirDTOProductoADatos(item);  
                    // listadoProductosDatos.Add(temp);
                    
                    // segunda opcion
                    listadoProductosDatos.Add(ConvertirDTOProductoADatos(item));
                }

                var resultado = intermedia.AgregarSucursalConProductos(sucursalDato, listadoProductosDatos);

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

        public BaseDTO ActualizarCorreoCliente (int idCliente, string correo)
        {
            try
            {
                var intermedia = new ClaseEjemploDatos(contexto);
                var resultado = intermedia.ActualizarCorreoCliente(idCliente, correo);
                if (resultado.CodigoRespuesta != -1)
                {
                   return new BaseDTO
                    {
                        Mensaje = "Se actualizó el correo del cliente " +
                                  ((Cliente)resultado.ContenidoRespuesta).DesCedula + " " +
                                  ((Cliente)resultado.ContenidoRespuesta).NomCliente 
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

        public List<BaseDTO> FiltrarProductosPorParametros(string nombreProducto, int tipoProducto,
                                                           int sucursal, List<decimal> rangoPrecios)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                
                var intermedio = new ClaseEjemploDatos(contexto);
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

                if (datosPrevios.Count > 0 )  // return data
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


        #endregion

        #endregion
    }
}
