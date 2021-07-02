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
    public class SolicitudDeCompraLogica
    {
        #region Variables
        DB_A4C98C_MusicStoreDBContext contexto = new DB_A4C98C_MusicStoreDBContext();
        #endregion

        #region Constructores
        public SolicitudDeCompraLogica()
        {

        }
        #endregion

        #region Metodos
        #region Conversiones
        internal static SolicitudCompraDTO ConvertirDatosOrdenCompraADTO(OrdenCompra ordenCompra)
        {
            return new SolicitudCompraDTO
            {
                ClienteAsociado = (ordenCompra.FkClienteNavigation != null ? ClienteLogica.ConvertirDatosClienteADTO(ordenCompra.FkClienteNavigation) : null),
                VendedorAsociado = (ordenCompra.FkVendedorNavigation != null ? VendedorLogica.ConvertirDatosVendedorADTO(ordenCompra.FkVendedorNavigation) : null),
                FechaOrden = ordenCompra.FecOrden,
                IdEntidad = ordenCompra.PkOrdenCompra,
                MontoTotal = ordenCompra.MntTotalOrden,
                TipoEntrega = ordenCompra.TipEntrega,
                EstadoSolicitud = ordenCompra.IndEstado,
                PctDescuento = ordenCompra.MtoPctDescuento
            };
        }

        internal static OrdenCompra ConvertirOrdenCompraDTOaDatos(SolicitudCompraDTO solicitudCompraDTO)
        {
            return new OrdenCompra
            {
                FecOrden = solicitudCompraDTO.FechaOrden,
                PkOrdenCompra = solicitudCompraDTO.IdEntidad,
                IndEstado = solicitudCompraDTO.EstadoSolicitud,
                MntTotalOrden = solicitudCompraDTO.MontoTotal,
                TipEntrega = solicitudCompraDTO.TipoEntrega,
                MtoPctDescuento = solicitudCompraDTO.PctDescuento
            };
        }

        #endregion

        #region Funcion
        public BaseDTO  ObtenerSolicitudCompraPorCodigo(int codigo)
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ObtenerSolicitudCompraPorCodigo(codigo);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    var solicitudCompraDTO = ConvertirDatosOrdenCompraADTO((OrdenCompra)respuestaDatos.ContenidoRespuesta);

                    return solicitudCompraDTO;
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

        public List<BaseDTO> ListarSolicitudesCompraPorEstado(int estado)
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesCompraPorEstado(estado);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respSolicitCompra;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListarSolicitudesCompraPorCliente(int cliente)
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesCompraPorCliente(cliente);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respSolicitCompra;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListarSolicitudesCompraPorVendedor(int vendedor)
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesCompraPorVendedor(vendedor);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respSolicitCompra;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListarSolicitudesCompraPorTipoEntrega(int tipEntrega)
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListarSolicitudesCompraPorTipoEntrega(tipEntrega);
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respSolicitCompra;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> ListarTotalSolicitudesCompra()
        {
            try
            {
                SolicitudCompraDatos intermedioEjemplo = new SolicitudCompraDatos();

                var respuestaDatos = intermedioEjemplo.ListarTotalSolicitudesCompra();
                if (respuestaDatos.CodigoRespuesta == 1)
                {
                    List<BaseDTO> respSolicitCompra = new List<BaseDTO>();
                    foreach (var item in (List<OrdenCompra>)respuestaDatos.ContenidoRespuesta)
                    {
                        respSolicitCompra.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respSolicitCompra;
                }
                else
                {
                    throw new Exception(((ErrorDTO)respuestaDatos.ContenidoRespuesta).MensajeError); // opcion 2
                }
            }
            catch (Exception error)
            {
                return new List<BaseDTO> { new ErrorDTO { MensajeError = error.Message } };
            }
        }

        public List<BaseDTO> FiltrarSolicitudesCompraPorParametros(int indEstado, int tipEntrega, 
                                                                   DateTime? fechaOrden)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {

                var intermedio = new SolicitudCompraDatos(contexto);
                var datosPrevios = new List<OrdenCompra>();

                if (indEstado != 0)
                {
                    datosPrevios = (List<OrdenCompra>)intermedio.FiltrarSolicitudesCompraPorParametros("indEstado", indEstado,
                                                                                                       datosPrevios).ContenidoRespuesta;
                }
                if (tipEntrega != 0)
                {
                    datosPrevios = (List<OrdenCompra>)intermedio.FiltrarSolicitudesCompraPorParametros("TipEntrega", tipEntrega,
                                                                        datosPrevios).ContenidoRespuesta;
                }

                if (fechaOrden != null)
                {
                    datosPrevios = (List<OrdenCompra>)intermedio.FiltrarSolicitudesCompraPorParametros("FecOrden", fechaOrden,
                                                    datosPrevios).ContenidoRespuesta;
                }

                

                if (datosPrevios.Count > 0)  // return data
                {
                    foreach (var item in datosPrevios)
                    {
                        respuesta.Add(ConvertirDatosOrdenCompraADTO(item));
                    }
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron resultados con los parametros establecidos");
                }
            }
            catch (Exception error)
            {
                respuesta.Clear();
                respuesta.Add(new ErrorDTO { MensajeError = error.Message });
                return respuesta;
            }
        }


        public BaseDTO AgregarSolicitudCompra(DateTime fecOrden, int tipEntrega, int idCliente, decimal mtoTotalOrden,
                                                   int indEstado)
        {
            try
            {
                var intermedia = new SolicitudCompraDatos (contexto);

                var resultado = intermedia.AgregarSolicitudCompra(fecOrden, tipEntrega, idCliente, mtoTotalOrden, 
                                                                  indEstado);

                if (resultado.CodigoRespuesta != -1)
                {
                    // exito
                    return new BaseDTO
                    {
                        Mensaje = resultado.Mensaje + " Se actualizó un total de " + resultado.ContenidoRespuesta +
                                  " datos."
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


        public BaseDTO AgregarSolicitudCompra(DateTime fecOrden, int tipEntrega, int idCliente, decimal mtoTotalOrden,
                                                   int idVendedor, int indEstado)
        {
            try
            {
                var intermedia = new SolicitudCompraDatos(contexto);

                var resultado = intermedia.AgregarSolicitudCompra(fecOrden, tipEntrega, idCliente, mtoTotalOrden, 
                                                                  idVendedor, indEstado); 

                if (resultado.CodigoRespuesta != -1)
                {
                    // exito
                    return new BaseDTO
                    {
                        Mensaje = resultado.Mensaje + " Se actualizó un total de " + resultado.ContenidoRespuesta +
                                  " datos."
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

        public BaseDTO ActualizarVendedorOrdenCompra(int idOrdenCompra, int idVendedor)
        {
            try
            {
                var intermedia = new SolicitudCompraDatos(contexto);
                var resultado = intermedia.ActualizarVendedorOrdenCompra(idOrdenCompra, idVendedor);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó el vendedor de la soicitud " +
                                   ((OrdenCompra)resultado.ContenidoRespuesta).FkVendedor
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

        public BaseDTO ActualizarEstadoOrdenCompra(int idOrdenCompra, int indEstado)
        {
            try
            {
                var intermedia = new SolicitudCompraDatos(contexto);
                var resultado = intermedia.ActualizarEstadoOrdenCompra(idOrdenCompra, indEstado);
                if (resultado.CodigoRespuesta != -1)
                {
                    return new BaseDTO
                    {
                        Mensaje = "Se actualizó el estado de la soicitud " +
                                   ((OrdenCompra)resultado.ContenidoRespuesta).IndEstado
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
        #endregion
        #endregion
    }
}
