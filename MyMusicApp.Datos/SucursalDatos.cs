using MyMusicApp.Datos.MyMusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class SucursalDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public SucursalDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public SucursalDatos()
        {

        }
        #endregion

        #region Método

        /// <summary>
        /// 2.a. Listado total de sucursales
        /// </summary>
        public object ListarTotalSucursales()
        {
            try
            {
                var sucursales = contexto.Sucursals.ToList();

                if (sucursales.Count > 0)
                {
                    return sucursales;
                }
                else
                {
                    throw new Exception("No se encontraron las sucursales [ListarTotalSucursales]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 2.b. Búsqueda de sucursal por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        public object ObtenerSucursalPorCodigo(int codigo)
        {
            try
            {
                var sucursal = contexto.Sucursals.FirstOrDefault(S => S.PkSucursal == codigo);

                if (sucursal != null)
                {
                    return sucursal;
                }
                else
                {
                    throw new Exception("No se encontró la sucursal por código en la base de datos [ObtenerSucursalPorCodigo]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 2.c. Búsqueda de sucursal por ubicación
        /// </summary>
        /// <param name="puesto"></param>
        /// <param name="codigoSucursal"></param>
        /// <returns></returns>
        public object ObtenerSucursalPorUbicacion(string ubicacion)
        {
            try
            {
                var sucursal = contexto.Sucursals.FirstOrDefault(S => S.DirUbicacion.Contains(ubicacion));

                if (sucursal != null)
                {
                    return sucursal;
                }
                else
                {
                    throw new Exception("No se encontró la sucursal por ubicación [ObtenerSucursalPorUbicacion]");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        #endregion
    }
}
