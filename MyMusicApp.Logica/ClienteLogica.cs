using MyMusicApp.Datos;
using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Logica
{
    public class ClienteLogica
    {

        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructor
        public ClienteLogica()
        {

        }
        #endregion

        #region Conversiones
        internal static ClienteDTO ConvertirDatosClienteADTO (Cliente cliente)
        {
            return new ClienteDTO
            {
                IdEntidad = cliente.PkCliente,
                Email = cliente.EmlDirCliente,
                FechaNacimiento = Convert.ToDateTime(cliente.FecNacimiento),
                IdCedula = cliente.DesCedula,
                IdContrasena = cliente.DesContrasena,
                Nombre = cliente.NomCliente,
                Sexo = cliente.TipSexo,
                Telefono = cliente.TelCliente,
                UsuarioCliente = cliente.UsrCliente
            };
        }

        internal static Cliente ConvertirDTOClienteADatos (ClienteDTO clienteDTO)
        {
            return new Cliente
            {
                DesCedula = clienteDTO.IdCedula,
                DesContrasena = clienteDTO.IdContrasena,
                EmlDirCliente =clienteDTO.Email,
                FecNacimiento =clienteDTO.FechaNacimiento,
                NomCliente = clienteDTO.Nombre,
                TelCliente = clienteDTO.Telefono,
                TipSexo = clienteDTO.Sexo,
                UsrCliente = clienteDTO.UsuarioCliente
                
            };
        }
        #endregion
        /*
        public BaseDTO ObtenerClientePorCedula(string cedula)
        {
            try
            {
                ClienteDatos intermedioEjemplo = new ClienteDatos(contexto);

                var respuestaDatos = intermedioEjemplo.ObtenerClientePorCedula(cedula);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var clienteRespuesta = ConvertirDatosClienteADTO((Cliente)respuestaDatos.ContenidoRespuesta);

                    return clienteRespuesta;
                    //Dato correcto
                }
                else
                {
                    //Dato incorrecto
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerClientePorCodigo(int codigo)
        {
            try
            {
                ClienteDatos intermedioEjemplo = new ClienteDatos(contexto);

                var respuestaDatos = intermedioEjemplo.ObtenerClientePorCodigo(codigo);

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var clienteRespuesta = ConvertirDatosClienteADTO((Cliente)respuestaDatos.ContenidoRespuesta);

                    return clienteRespuesta;
                    //Dato correcto
                }
                else
                {
                    //Dato incorrecto
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public List<BaseDTO> ListarTotalClientes()
        {
            try
            {
                ClienteDatos intermedioDatos = new ClienteDatos(contexto);

                var respuestaDatos = intermedioDatos.ListarTotalClientes();

                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respuestaClientes = new List<BaseDTO>();
                    // Llamada exitosa
                    foreach (var item in (List<Cliente>)respuestaDatos.ContenidoRespuesta)
                    {
                        respuestaClientes.Add(ConvertirDatosClienteADTO(item));
                    }

                    return respuestaClientes;
                }
                else
                {
                    // Error controlado
                    //return new List<BaseDTO> { (ErrorDTO)respuestaDatos.ContenidoRespuesta };
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError);
                }
            }
            catch (Exception error)
            {
                //Error no controlado
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }
        */
        public BaseDTO AgregarCliente(ClienteDTO cliente)
        {
            try
            {
                var intermedia = new ClienteDatos(contexto);

                var clienteDat = ConvertirDTOClienteADatos(cliente);

                var resultado = intermedia.AgregarCliente(clienteDat);

                if (resultado.CodigoRespuesta != -1)
                {
                    //caso de éxito
                    return new BaseDTO
                    {
                        IdEntidad = Convert.ToInt32(resultado.ContenidoRespuesta),
                        Mensaje = "Se insertaron correctamente los datos."
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

        public BaseDTO ActualizarCorreoCliente(int idCliente, string correo)
        {
            try
            {
                var intermedia = new ClienteDatos(contexto);

                var resultado = intermedia.ActualizarCorreoCliente(idCliente, correo);

                if (resultado.CodigoRespuesta != -1)
                {
                    //Caso de éxito
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó la información del cliente: " + ((Cliente)resultado.ContenidoRespuesta).NomCliente
                    };
                }
                else
                {
                    //Error controlado
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ActualizarDatoContactoCliente(int idCliente, string telefono, string email)
        {
            try
            {
                var intermedia = new ClienteDatos(contexto);

                var resultado = intermedia.ActualizarDatoContactoCliente(idCliente, telefono, email);

                if (resultado.CodigoRespuesta != -1)
                {
                    //Caso de éxito
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó la información del cliente: " + ((Cliente)resultado.ContenidoRespuesta).NomCliente
                    };
                }
                else
                {
                    //Error controlado
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ActualizarDatoAccesoCliente(int idCliente, string usuario, string contrasena)
        {
            try
            {
                var intermedia = new ClienteDatos(contexto);

                var resultado = intermedia.ActualizarDatoContactoCliente(idCliente, usuario, contrasena);

                if (resultado.CodigoRespuesta != -1)
                {
                    //Caso de éxito
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó la información del cliente: " + ((Cliente)resultado.ContenidoRespuesta).NomCliente
                    };
                }
                else
                {
                    //Error controlado
                    return (ErrorDTO)resultado.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }

        public BaseDTO ObtenerClientePorCodigo(int idCliente)
        {
            try
            {
                // Si estoy seguro que voy a utilizar la clase de sucursales, puedo dejar el contexto vació. 

                ClienteDatos intermedio = new ClienteDatos();
                var respuestaDatos = intermedio.ObtenerClientePorCodigo(idCliente);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var clienteRespuesta = ConvertirDatosClienteADTO((Cliente)respuestaDatos.ContenidoRespuesta);

                    return clienteRespuesta;
                }
                else
                {
                    return (ErrorDTO)respuestaDatos.ContenidoRespuesta;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO { MensajeError = error.Message };
            }
        }
    }
}
