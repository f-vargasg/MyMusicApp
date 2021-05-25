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

        // desarrollar otro metodo GET que nos permita listar las sucursales de la base DTO (sin parametros)
        // Sin utilizaqr GET que viene por defecto en la aplicación

        // POST api/<MyMusicAppServicesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
    }
}
