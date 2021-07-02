using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusicApp.DTO;
using MyMusicApp.Logica;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMusicApp.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMusicAppServicesController : ControllerBase
    {
        // GET: api/<MyMusicAppServicesController>
        [HttpGet]
        public IEnumerable<BaseDTO> Get()
        {
            // ProductoLogica productoLogica = new ProductoLogica();
            //var resultado = productoLogica.ListarProductos();
            // var resultadoConvertido = resultado.OfType<ProductoDTO>();
            // return resultadoConvertido;

            // forma rapida 
            var resultado = new ProductoLogica().ListarProductos().OfType<ProductoDTO>();

            return resultado;
        }

        // GET api/<MyMusicAppServicesController>/5
        [HttpGet("{id}")]
        public IEnumerable<BaseDTO> Get(int id)
        {
            var resultado = new ClaseEjemploLogica().ObtenerProductoPorTipo(id);

            if (resultado.ElementAt(0).GetType() == typeof(ErrorDTO))
            {
                return resultado.OfType<ErrorDTO>();
            }
            else
            {
                return resultado.OfType<ProductoDTO>();
            }

            // return new ClaseEjemploLogica().ObtenerProductoPorTipo(id).OfType<ProductoDTO>();
        }

        // 

        // desarrollar otro metodo GET que nos permita listar las sucursales de la base DTO (sin parametros)
        // Sin utilizaqr GET que viene por defecto en la aplicación

        // En estas clases de ejemplo no existía el método de listar sucursales se utilizará
        // se utilizará 

        [HttpGet("GetSucursales")]
        public IEnumerable<BaseDTO> GetSucursales()
        {
            return new SucursalLogica().ListarTotalSucursales().OfType<SucursalDTO>();
        }

        // POST api/<MyMusicAppServicesController>

        /*[HttpPost]
         * public void Post([FromBody] string value)
        {
        } */


        /// <summary>
        /// Respuesta 2.4
        /// </summary>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        [HttpGet("GetSolicitEnvioPorEstado")]
        public IEnumerable<BaseDTO> GetSolicitEnvioPorEstado(int indEstado)
        {
            return new SolicitudEnvioLogica().ListarSolicitudesEnvioPorEstado(indEstado).OfType<SolicitudEnvioDTO>();
        }

        public BaseDTO Post(int idCliente, string correo)
        {
            return new ClaseEjemploLogica().ActualizarCorreoCliente(idCliente, correo);
        }

        /// <summary>
        /// Respuesta 1.4
        /// </summary>
        /// <param name="vendedorDTO"></param>
        /// <returns></returns>
        [HttpPost("PostActVendPorPrimVez")]
        public BaseDTO PostActVendPorPrimVez (VendedorDTO vendedorDTO)
        {
            return new VendedorLogica().RegistrarVendedor(vendedorDTO);
        }

        /// <summary>
        /// 2.4
        /// </summary>
        /// <param name="desUbicacion"></param>
        /// <param name="fecEnvio"></param>
        /// <param name="fecRecibo"></param>
        /// <param name="idOrdenCompra"></param>
        /// <param name="indEstado"></param>
        /// <returns></returns>
        [HttpPost("PostAgregarSolEnvPrimVez")]
        public BaseDTO PostAgregarSolEnvPrimVez(string desUbicacion, DateTime fecEnvio, DateTime fecRecibo,
                                                  int idOrdenCompra, int indEstado)
        {
            return new SolicitudEnvioLogica().AgregarSolicitudEnvio(desUbicacion, fecEnvio, fecRecibo, idOrdenCompra,
                                                                     indEstado);
        }

        
        // PUT api/<MyMusicAppServicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MyMusicAppServicesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //Método para obtener los productos “de segunda”
        [HttpGet("GetProductosSegunda")]
        public IEnumerable<BaseDTO> GetProductosSegunda()
        {
            return new ProductoLogica().ListarProductosDeSegunda().OfType<ProductoDTO>();
        }

        //Método para obtener los productos “de segunda” relacionados a los productos nuevos
        [HttpGet("GetProdSegundaRelProdNuevo")]
        public BaseDTO GetProdSegundaRelProdNuevo(string nomProducto)
        {
            return new ProductoLogica().ListarProductoDeSegundaParecidosA(nomProducto);
        }
    }
}
