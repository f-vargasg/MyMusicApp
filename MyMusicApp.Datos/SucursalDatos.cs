using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
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
        MusicStoreDBContext contexto = new MusicStoreDBContext();
        #endregion

        #region Constructor
        public SucursalDatos(MusicStoreDBContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        public SucursalDatos()
        {

        }
        #endregion

        #region Método

        public RespuestaDTO AgregarSucursal(string ubicacion, string horario, string telefono, string correo)
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
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = guardado,
                        Mensaje = "Los datos se guardaron correctamente"
                    };
                }
                else
                {
                    throw new Exception("No se pudo guardar la sucursal, por favor revisar los datos suministrados");
                }

                // return true;
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

        public RespuestaDTO ActualizarDatosSucursal(int codigo, string ubicacion, string telefono, string correo,
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


                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            CodigoRespuesta = 1,
                            ContenidoRespuesta = sucursal
                        };
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
                // return true;
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
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

        /// <summary>
        /// 2.a. Listado total de sucursales
        /// </summary>
        public RespuestaDTO ListarTotalSucursales()
        {
            try
            {
                var sucursales = contexto.Sucursals.ToList();

                if (sucursales.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = sucursales
                    };
                }
                else
                {
                    throw new Exception("No se encontraron las sucursales [ListarTotalSucursales]");
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

        /// <summary>
        /// 2.b. Búsqueda de sucursal por Primary Key
        /// </summary>
        /// <param name="codigo"></param>
        public RespuestaDTO ObtenerSucursalPorCodigo(int codigo)
        {
            try
            {
                var sucursal = contexto.Sucursals.FirstOrDefault(S => S.PkSucursal == codigo);

                if (sucursal != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = sucursal
                    };
                }
                else
                {
                    throw new Exception("No se encontró la sucursal por código en la base de datos [ObtenerSucursalPorCodigo]");
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

        /// <summary>
        /// 2.c. Búsqueda de sucursal por ubicación
        /// </summary>
        /// <param name="puesto"></param>
        /// <param name="codigoSucursal"></param>
        /// <returns></returns>
        public RespuestaDTO ObtenerSucursalPorUbicacion(string ubicacion)
        {
            try
            {
                var sucursal = contexto.Sucursals.FirstOrDefault(S => S.DirUbicacion.Contains(ubicacion));

                if (sucursal != null)
                {
                    return new RespuestaDTO
                    {
                        CodigoRespuesta = 1,
                        ContenidoRespuesta = sucursal
                    };
                }
                else
                {
                    throw new Exception("No se encontró la sucursal por ubicación [ObtenerSucursalPorUbicacion]");
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
