using MyMusicApp.Datos.MyMusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Datos
{
    public class ClienteDatos
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public ClienteDatos(DB_A4C98C_MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        public ClienteDatos()
        {

        }
        #endregion

        #region Metodos

        /// <summary>
        /// 1.a. Búsqueda del cliente por cédula
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        public object ObtenerClientePorCedula(string cedula)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(P => P.DesCedula == cedula);

                if (cliente != null)
                {
                    return cliente;
                }
                else
                {
                    throw new Exception("No se encontró cliente con la cedula suministrada");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 1.b. Búsqueda del cliente por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerClientePorCodigo(int codigo)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(P => P.PkCliente == codigo);

                if (cliente != null)
                {
                    return cliente;
                }
                else
                {
                    throw new Exception("No se encontró el cliente con el código suministrado");
                }
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// 3.c. Listado de la totalidad de los clientes
        /// </summary>
        /// <returns></returns>
        public object ListarTotalClientes()
        {
            try
            {
                var clientes = contexto.Clientes.ToList();
                if (clientes.Count > 0)
                {
                    return clientes;
                }
                else
                {
                    throw new Exception("No se encontraron los clientes");
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
