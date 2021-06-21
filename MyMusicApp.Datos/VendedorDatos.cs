using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    //Escribir las tres regiones principales: Variables, Constructor y Métodos con sus respectivos componentes.
    // # 1 -> Obtener los Vendedores por código o pk
    // # 2 -> Obtener los Vendedores de una sucursal determinada
    // # 3 -> Obtener los vendedores de una sucursal determinada según su puesto
    // # 4 -> Filtrar los vendedores ya sea por el nombre, la sucursal o el puesto 
    public class VendedorDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public VendedorDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public VendedorDatos()
        {

        }
        #endregion

        #region Método

        public RespuestaDTO AgregarVendedor(Vendedor vendedor)
        {
            try
            {
                contexto.Vendedors.Add(vendedor);
                var guardado = contexto.SaveChanges();
                if (guardado > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = guardado,
                        Mensaje = "Los datos se guardaron correctamente"

                    };
                }
                else
                {
                    throw new Exception("No se pudo insertar cliente");
                }
            }
            catch (Exception error)
            {
                if (error.Message.Contains("ERROR controlado"))
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
                        ContenidoRespuesta = new ErrorDTO { MensajeError = "ERROR NO CONTROLADO" + error.InnerException }
                    };
                }
            }
        }

        /// <summary>
        /// 4.a Listado total de vendedores
        /// </summary>
        public object ListarTotalVendedores()
        {
            try
            {
                var vendedores = contexto.Vendedors.ToList();

                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    throw new Exception("No se encontraron los vendedores [ListarTotalVendedores]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 4.b. Búsqueda de vendedor por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        public RespuestaDTO ObtenerVendedorPorCodigo(int codigo)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(V => V.PkVendedor == codigo);

                if (vendedor != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = vendedor
                    };
                }
                else
                {
                    throw new Exception("No se encontraron vendedores por código en la base de datos [ObtenerVendedorPorCodigo]");
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

        /// <summary>
        /// 4.c. Búsqueda de vendedor según su puesto
        /// </summary>
        /// <param name="puesto"></param>
        /// <returns></returns>
        public object ListarVendedoresPorPuesto(string puesto)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(V => V.DesPuesto.Contains(puesto)).ToList();

                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    throw new Exception("No se encontraron vendedores por puesto [ListarVendedoresPorPuesto]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 4.d Búsqueda de vendedor según la sucursal a la que pertenecen
        /// </summary>
        /// <param name="codigoSucursal"></param>
        public object ListarVendedorPorSucursal(int codigoSucursal)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(V => V.FkSucursal == codigoSucursal).ToList();

                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    throw new Exception("No se encontraron vendedores por sucursal [ListarVendedorPorSucursal]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// HECHOS ANTERIORMENTE
        /// obtener los vendedores de una sucursal determinada segun su puesto
        /// </summary>
        /// <param name="puesto"></param>
        /// <param name="codigoSucursal"></param>
        /// <returns></returns>
        public object ListarVendedoresSucursalPuesto(string puesto, int codigoSucursal)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(V => V.FkSucursal == codigoSucursal && V.DesPuesto.Contains(puesto)).ToList();

                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    throw new Exception("No se encontraron venderores por sucursal y puesto [ListarVendedoresSucursalPuesto]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }


        /// <summary>
        /// HECHOS ANTERIORMENTE
        /// //#4 filtrar los vendedores ya sea por el nombre, la sucursal, el puesto
        /// </summary>
        /// <param name="nombreParametro"></param>
        /// <param name="datoParametro"></param>
        /// <param name="datosPrevios"></param>
        /// <returns></returns>
        public object FiltrarVendedoresPorParametros(string nombreParametro, object datoParametro, List<Vendedor> datosPrevios)
        {
            try
            {
                List<Vendedor> respuesta = new List<Vendedor>();

                int valorInt = 0;

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Nombre":
                            datosPrevios = datosPrevios.Where(V => V.NomVendedor.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            datosPrevios = datosPrevios.Where(V => V.FkSucursal == valorInt).ToList();
                            break;
                        case "Puesto":
                            datosPrevios = datosPrevios.Where(V => V.DesPuesto.Contains(datoParametro.ToString())).ToList();
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
                            respuesta = contexto.Vendedors.Where(V => V.NomVendedor.Contains(datoParametro.ToString())).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParametro);
                            respuesta = contexto.Vendedors.Where(V => V.FkSucursal == valorInt).ToList();
                            break;
                        case "Puesto":
                            respuesta = contexto.Vendedors.Where(V => V.DesPuesto.Contains(datoParametro.ToString())).ToList();
                            break;
                        default:
                            break;
                    }
                    return respuesta;
                }
            }
            catch (Exception er)
            {
                return er.Message;
            }
        }

        // Laboratorio #1 - 4.a. Registro del vendedor por primera vez, asignándolo a una sucursal (insert)
        public RespuestaDTO RegistrarVendedorAsignadoSucursal(Vendedor vendedor)
        {
            try
            {
                contexto.Vendedors.Add(vendedor);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = vendedor.PkVendedor,
                        Mensaje = "Datos guardados con exito"
                    };
                }
                else
                {
                    throw new Exception("No se encontró el vendedor especificado");
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
