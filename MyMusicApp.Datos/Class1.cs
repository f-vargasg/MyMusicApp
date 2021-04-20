using System;
using MyMusicApp.Datos.MyMusicModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyMusicApp.Datos
{
    public class Class1
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public Class1(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public Class1()
        {

        }
        #endregion

        #region Metodos
        public object ListarProductos ()
        {
            try
            {
                //                 1       2         3
                var productos = contexto.Productos.ToList();
                if (productos.Count> 0)
                {
                    return productos;
                }
                else 
                {
                    throw new Exception("No se encontraron productos en la base de datos");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public object ListarProductosPorTipo(int tipo)
        {
            try
            {
                var productosPorTipo = contexto.Productos.Where(P => P.TipProducto == tipo).ToList();
                if (productosPorTipo.Count >0)
                {
                    return productosPorTipo;
                }
                else
                {
                    throw new Exception("No se encontraron productos para el tipo indicado en la base de datos");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object ObtenerProductoPorCodigo(int codigo)
        {
            try
            {
                //                      1      2        3               4                    3*       
                // var producto = contexto.Productos.Where(P => P.PkProducto == codigo).FirstOrDefault();
                
                //                      1      2        3               4                    3*       
                var producto = contexto.Productos.FirstOrDefault(P => P.PkProducto == codigo);
                if (producto != null)
                {
                    return producto;
                }
                else 
                {
                    throw  new Exception("No se encontraron productos con el código suministrato");
                }

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public void ListarProductosPorSucursal(int codigoSucursal)
        {
            try
            {
                var productos = contexto.Productos.Where(P => P.FkSucursal == codigoSucursal).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ListarTipoProductoPorSucursal (int tipo, int codigoSucursal)
        {
            try
            {
                // var productos = contexto.Productos.Where(P => P.TipProducto == tipo &&P.FkSucursal == codigoSucursal).Include(S => ).ToList();
                var sucursales = contexto.Sucursals.Where(S => S.PkSucursal == codigoSucursal &&
                                                          S.Productos.Where(P => P.TipProducto == tipo).Count() > 0).
                                                          Include(P => P.Productos).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void ListarClientesPorSexo(string sexo)
        {
            try
            {
                var productosPorTipo = contexto.Clientes.Where(C => C.TipSexo == sexo).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ListarVendedoresPorSucursal(int codigoSucursal)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(V => V.FkSucursal == codigoSucursal).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ObtenerVendedor(int codigoVendedor)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(V => V.PkVendedor == codigoVendedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Método con parametros anónimos
        public object FiltrarProductosPorParametros (string nombreParametro, object datoParemtro, List<Producto> datosPrevios)
        {
            try
            {
                List<Producto> respuesta = new List<Producto>();
                int valorInt = 0;
                string valorString = string.Empty;
                List<decimal> valoresDecimal = new List<decimal>();

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Nombre":
                            datosPrevios = datosPrevios.Where(P => P.NomProducto.Contains(datoParemtro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParemtro);
                            datosPrevios = datosPrevios.Where(P => (P.TipProducto == valorInt)).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParemtro);
                            datosPrevios = datosPrevios.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Rango":
                            
                            valoresDecimal = (List<decimal>)datoParemtro;
                            /*datosPrevios = datosPrevios.Where(P => P.MtoPrecioUnitario >= valoresDouble.ElementAt(0)
                                                                      && P.MtoPrecioUnitario <= valoresDouble.ElementAt(1)).ToList(); */
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
                        case "Nombre":
                            respuesta = contexto.Productos.Where(P => P.NomProducto.Contains(datoParemtro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParemtro);
                            respuesta = contexto.Productos.Where(P => (P.TipProducto == valorInt)).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParemtro);
                            respuesta = contexto.Productos.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Rango":
                            valoresDecimal = (List<decimal>)datoParemtro;
                            respuesta = contexto.Productos.Where(P => P.MtoPrecioUnitario >= valoresDecimal.ElementAt(0)
                                                                      && P.MtoPrecioUnitario <= valoresDecimal.ElementAt(1)).ToList(); 
                            break;
                        default:
                            break;
                    }
                }
                return respuesta;
            }
            catch (Exception error)
            {
                return  error.Message;
                throw;
            }
        }
        

        // Metodo con parametros opcionales
        public object FiltrarProductosPorParametros(string nombre = null, int tipo = 0, int sucursal = 0, 
                                                      List<decimal> rangos = null)
        {
            try
            {
                List<Producto> respuesta = new List<Producto>();

                if (nombre != null)
                {
                    respuesta = contexto.Productos.Where(P => P.NomProducto.Contains(nombre)).ToList();
                }

                if (tipo != 0)
                {
                    if (respuesta.Count > 0)
                    {
                        respuesta = respuesta.Where(P => P.TipProducto == tipo).ToList();
                    }
                    else
                    {
                        respuesta = contexto.Productos.Where(P => P.TipProducto == tipo).ToList();
                    }
                }

                if (sucursal != 0)
                {
                    if (respuesta.Count > 0)
                    {
                        respuesta = respuesta.Where(P => P.FkSucursal == sucursal).ToList();
                    }
                    else
                    {
                        respuesta = contexto.Productos.Where(P => P.FkSucursal == sucursal).ToList();
                    }
                }

                if (rangos != null)
                {
                    if (respuesta.Count > 0)
                    {
                        respuesta = respuesta.Where(P => P.MtoPrecioUnitario >= rangos.ElementAt(0) &&
                                                         P.MtoPrecioUnitario <= rangos.ElementAt(1)).ToList();
                    }
                    else
                    {
                        respuesta = contexto.Productos.Where(P => P.MtoPrecioUnitario >= rangos.ElementAt(0) &&
                                                             P.MtoPrecioUnitario <= rangos.ElementAt(1)).ToList();
                    }
                }
                return respuesta;
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }
        #endregion

    }
}
