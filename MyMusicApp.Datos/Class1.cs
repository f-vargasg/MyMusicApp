using System;
using MyMusicApp.Datos.MyMusicModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public void ListarProductos ()
        {
            try
            {
                //                 1       2         3
                var productos = contexto.Productos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ListarProductosPorTipo(int tipo)
        {
            try
            {
                var productosPorTipo = contexto.Productos.Where(P => P.TipProducto == tipo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ObtenerProductoPorCodigo(int codigo)
        {
            try
            {
                //                      1      2        3               4                    3*       
                // var producto = contexto.Productos.Where(P => P.PkProducto == codigo).FirstOrDefault();
                
                //                      1      2        3               4                    3*       
                var producto = contexto.Productos.FirstOrDefault(P => P.PkProducto == codigo);
            }
            catch (Exception)
            {
                throw;
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
        #endregion

    }
}
