﻿using System;
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
        public object ListarProductos()
        {
            try
            {
                //                 1       2         3
                var productos = contexto.Productos.ToList();
                if (productos.Count > 0)
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
                if (productosPorTipo.Count > 0)
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
                    throw new Exception("No se encontraron productos con el código suministrato");
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

        public void ListarTipoProductoPorSucursal(int tipo, int codigoSucursal)
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
        public object FiltrarProductosPorParametros(string nombreParametro, object datoParametro, List<Producto> datosPrevios)
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
                            datosPrevios = datosPrevios.Where(P => P.NomProducto.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(P => (P.TipProducto == valorInt)).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Rango":
                            valoresDecimal = (List<decimal>)datoParametro;
                            /*datosPrevios = datosPrevios.Where(P => P.MtoPrecioUnitario >= valoresDouble.ElementAt(0)
                                                                      && P.MtoPrecioUnitario <= valoresDouble.ElementAt(1)).ToList(); */
                            datosPrevios = datosPrevios.Where(P => P.MtoPrecioUnitario >= valoresDecimal.ElementAt(0)
                                          && P.MtoPrecioUnitario <= valoresDecimal.ElementAt(1)).ToList(); 
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
                            respuesta = contexto.Productos.Where(P => P.NomProducto.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Tipo":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.Productos.Where(P => (P.TipProducto == valorInt)).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.Productos.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Rango":
                            valoresDecimal = (List<decimal>)datoParametro;
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
                return error.Message;
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


        public object AgregarSucursal(string ubicacion, string horario, string telefono, string correo)
        {
            try
            {
                var sucursal = new Sucursal
                {
                    DirUbicacion = ubicacion,
                    DesHorario = horario,
                    TelSucursal = telefono,
                    EmlSucursal = correo,
                };
                contexto.Sucursals.Add(sucursal);

                var guardado = contexto.SaveChanges();

                if (guardado > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se pudo guardar la sucursal, por favor revisar los datos suministrados");
                }
    
                return true;
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }


        public object ActualizarDatosSucursal(int codigo, string ubicacion, string telefono, string correo,
                                              string horario)
        {
            try
            {
                var sucursal = contexto.Sucursals.FirstOrDefault(S => S.PkSucursal == codigo);
                if (sucursal != null)
                {
                    // Hacemos la actualizacion
                    sucursal.DirUbicacion = ubicacion;
                    sucursal.TelSucursal = telefono;
                    sucursal.EmlSucursal = correo;
                    sucursal.DesHorario = horario;

                    
                    if (contexto.SaveChanges() >0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar la sucursal, por favor revise los datos suministrados");
                    } 

                }
                else
                {
                    throw new Exception("No se encontró la sucursal con el código suministrado");
                }
                return true;
            }
            catch (Exception error)
            {
               return  error.Message;
            }
        }

        public object RegistrarSucursalConVendedorEncargado (string telefonoSucursal, string ubicacionSucursal,
                                                           string correoSucursal, string horarioSucursal,
                                                           string nombreVendedor, string ptoVendedor,
                                                           string userVendedor, string passVendedor)
        {
            try
            {
                var sucursal = new Sucursal
                {
                    DirUbicacion = ubicacionSucursal,
                    DesHorario = horarioSucursal,
                    TelSucursal = telefonoSucursal,
                    EmlSucursal = correoSucursal
                };

                contexto.Sucursals.Add(sucursal);
                sucursal.Vendedors.Add(
                    new Vendedor
                    {
                        NomVendedor = nombreVendedor,
                        DesPuesto = ptoVendedor,
                        UsrVendedor = userVendedor,
                        UsrPassword = userVendedor
                    });
                ;
                var guardado  = contexto.SaveChanges();
                if (guardado > 0 )
                {
                    if (guardado == 1)
                    {
                        throw new Exception("Se guardó la información de la sucursal, pero ocurrió un error guardando el vendedor");
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    throw new Exception("No se puedo guardar la información suministrada");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        public object AgregarCliente(Cliente cliente)
        {
            try
            {
                contexto.Clientes.Add(cliente);
                if (contexto.SaveChanges() >0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se pudo insertar cliente");
                }
            }
            catch (Exception error)
            {

                return error.Message;
            }
        }

        // forma "compleja"
        // En este método vamos a insertar la sucursal y el producto como uno solo,
        // sino se inserta el producto no se inserta el producto
        public object AgregarSucursalConProductos(string telefonoSucursal, string ubicacionSucursal,
                                                  string correoSucursal, string horarioSucursal,
                                                  string nombreProducto, int tipoProducto, int cantidadProducto,
                                                  decimal montoProducto)
        {
            try
            {
                List<Producto> productosInsertar = new List<Producto>
                {
                    new Producto
                    {
                        CntProducto = cantidadProducto,
                        TipProducto = tipoProducto,
                        MtoPrecioUnitario = montoProducto,
                        NomProducto = nombreProducto

                    }
                };

                var sucursal = new Sucursal
                {
                    DirUbicacion = ubicacionSucursal,
                    EmlSucursal = correoSucursal,
                    DesHorario = horarioSucursal,
                    TelSucursal = telefonoSucursal,
                    Productos = productosInsertar
                };

                contexto.Sucursals.Add(sucursal);

                if (contexto.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se pudo insertar Sucursal y sus productos");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        // Método simplificado
        // En este método vamos a insertar la sucursal y el producto como uno solo,
        // sino se inserta el producto no se inserta el producto
        public object AgregarSucursalConProductos(Sucursal sucursal, List<Producto> productos)
        {
            try
            {
                sucursal.Productos = productos;
                contexto.Sucursals.Add(sucursal);
                contexto.Sucursals.Add(sucursal);

                if (contexto.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se pudo insertar Sucursal y sus productos");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        // forma compleja
        public object ActualizarCorreoCliente (int idCliente, string correo)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(C => C.PkCliente == idCliente);
                if (cliente != null)
                {
                    cliente.EmlDirCliente = correo;
                    if (contexto.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return new Exception("No se pudo actualizar el correo del cliente");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el cliente especificado");
                }
            }
            catch (Exception ex)
            {
                return ex.Message; ;
            }
        }

        // forma recomendada, pero a nivel de recursos NO es recomendable
        // entity framework evalua "cliente" y como no está completo, es 
        // un overhead
        public object ActualizarCorreoCliente(Cliente cliente)
        {
            try
            {
                // se asume que viene el pk y el correo del cliente (el resto del 
                var clienteModificar = contexto.Clientes.FirstOrDefault(C => C.PkCliente == cliente.PkCliente);
                if (clienteModificar != null)
                {
                    clienteModificar.EmlDirCliente = cliente.EmlDirCliente;
                    if (contexto.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return new Exception("No se pudo actualizar el correo del cliente");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el cliente especificado");
                }
            }
            catch (Exception ex)
            {
                return ex.Message; ;
            }
        }
        #endregion



    }

}



