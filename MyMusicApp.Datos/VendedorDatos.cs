using MyMusicApp.Datos.MyMusicModel;
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

        #region Constructores
        public VendedorDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        public VendedorDatos()
        {

        }
        #endregion

        #region Metodos
        public object ObtenerVendedorePorCodigo(int codigo)
        {
            try
            {
                //                      1      2        3               4                    3*       
                // var producto = contexto.Productos.Where(P => P.PkProducto == codigo).FirstOrDefault();

                //                      1      2        3               4                    3*       
                var vendedor = contexto.Vendedors.FirstOrDefault(P => P.PkVendedor == codigo);
                if (vendedor != null)
                {
                    return vendedor;
                }
                else
                {
                    throw new Exception("No se encontraron vendedores con el código suministrato");
                }

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public  object ListarVendedorPorSucursal(int codigoSucursal)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(P => P.FkSucursal == codigoSucursal).ToList();
                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    return new Exception("No existe Vendedor por sucursal");
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object ListarVendedorPorSucursalPorPuesto(int codigoSucursal, string desPuesto)
        {
            try
            {
                // var productos = contexto.Productos.Where(P => P.TipProducto == tipo &&P.FkSucursal == codigoSucursal).Include(S => ).ToList();
                var vendedores = contexto.Vendedors.Where(S => S.FkSucursal == codigoSucursal &&
                                                               S.DesPuesto == desPuesto).ToList();
                if (vendedores.Count > 0)
                {
                    return vendedores;
                }
                else
                {
                    return new Exception("No existen Vendedores por codigo sucursal y puesto");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object ListarVendedoresPorNombreSucursalPuesto(string nombreParametro, object datoParemtro, List<Vendedor> datosPrevios)
        {
            try
            {
                List<Vendedor> respuesta = new List<Vendedor>();
                int valorInt = 0;
                string valorString = string.Empty;
                List<decimal> valoresDecimal = new List<decimal>();

                if (datosPrevios.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "Nombre":
                            datosPrevios = datosPrevios.Where(P => P.NomVendedor.Contains(datoParemtro.ToString())).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParemtro);
                            datosPrevios = datosPrevios.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Puesto":
                            valorString = Convert.ToString(datoParemtro);
                            datosPrevios = datosPrevios.Where(P => P.DesPuesto ==valorString).ToList();
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
                            respuesta = contexto.Vendedors.Where(P => P.NomVendedor.Contains(datoParemtro.ToString())).ToList();
                            break;
                        case "Sucursal":
                            valorInt = Convert.ToInt32(datoParemtro);
                            respuesta = contexto.Vendedors.Where(P => (P.FkSucursal == valorInt)).ToList();
                            break;
                        case "Puesto":
                            valorString = Convert.ToString(datoParemtro);
                            respuesta = contexto.Vendedors.Where(P => P.DesPuesto == valorString).ToList();
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
        #endregion
    }
}
