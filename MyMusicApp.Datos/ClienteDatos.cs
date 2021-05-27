using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
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

        public RespuestaDTO ActualizarCorreoCliente(int idCliente, string correo)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(C => C.PkCliente == idCliente);
                if (cliente != null)
                {
                    cliente.EmlDirCliente = correo;
                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = cliente
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el correo del cliente");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el cliente especificado");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.InnerException.Message }
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.Message }
                    };
                }
            }
        }

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

        /// <summary>
        /// 1.a Registro de un cliente por primera vez
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public RespuestaDTO AgregarCliente(Cliente cliente)
        {
            try
            {
                contexto.Clientes.Add(cliente);
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
        /// 1.b Actualizar datos 
        /// </summary>
        /// <returns></returns>
        public RespuestaDTO ActualizarDatoContactoCliente(int idCliente, string telefono, string email)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(C => C.PkCliente == idCliente);
                if (cliente != null)
                {
                    cliente.TelCliente = telefono;
                    cliente.EmlDirCliente = email;
                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = cliente
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el Contacto del cliente");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el cliente especificado");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.InnerException.Message }
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.Message }
                    };
                }
            }
        }

        /// <summary>
        /// 1.c Actualización de datos de acceso del cliente (Usuario, Contraseña).
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="usuario"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public RespuestaDTO ActualizarDatoAccesoCliente(int idCliente, string usuario, string contrasena)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(C => C.PkCliente == idCliente);
                if (cliente != null)
                {
                    cliente.UsrCliente = usuario;
                    cliente.EmlDirCliente = contrasena;
                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = cliente
                        };
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el Contacto del cliente");
                    }
                }
                else
                {
                    throw new Exception("No se encontró el cliente especificado");
                }
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = -1,
                        ContenidoRespuesta = new ErrorDTO { MensajeError = error.InnerException.Message }
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
