using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicApp.DTO;
using MyMusicApp.Logica;
using MyMusicApp.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApp.Web.Controllers
{
    public class FuncionarioController : Controller
    {

        public ActionResult CreateVendedor()
        {
            ViewBag.PuestoVendedor = "Puesto Vendedor: <NO ASIGNADO>";
            if (HttpContext.Session.GetString("PuestoVendedor") != null)
            {
                ViewBag.PuestoVendedor = "Puesto Vendedor: " + HttpContext.Session.GetString("PuestoVendedor");
            }
            ViewBag.FechaHora = "La fecha y hora del sistema es: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            return View();
        }

        /// <summary>
        /// QUIZ #2 - 2.b.i
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVendedor(VendedorDTO model)
        {
            try
            {
                var resultado = new VendedorLogica().RegistrarVendedor(model);

                if (resultado.GetType() == typeof(ErrorDTO))
                {
                    throw new Exception("Error");
                }
                else
                {
                    return RedirectToAction("DetailsVendedor", new { id = resultado.IdEntidad });
                }
            }
            catch
            {
                return View("Error");
            }
        }

        /// <summary>
        /// QUIZ #2 - 2.b.iv    Búsqueda de vendedor por Primary Key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DetailsVendedor(int id)
        {
            VendedorVM model = new VendedorVM();

            ViewBag.PuestoVendedor = "Puesto Vendedor: <NO ASIGNADO>";
            if (HttpContext.Session.GetString("PuestoVendedor") != null)
            {
                ViewBag.PuestoVendedor = "Puesto Vendedor: " + HttpContext.Session.GetString("PuestoVendedor");
            }


            var resultado = new VendedorLogica().ObtenerVendedorPorCodigo(new VendedorDTO { IdEntidad = id });

            if (resultado.GetType() == typeof(ErrorDTO))
            {
                model.Error = (ErrorDTO)resultado;
            }
            else
            {
                model.Vendedor = (VendedorDTO)resultado;
            }

            return View(model);
        }
        /// <summary>
        /// Tarea 2 (17 junio 2021)
        /// Pregunta 1.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPuestoVendedor(VendedorVM model)
        {
            // Leer el puesto del vendedor
            VendedorDTO vendedorDTO = new VendedorDTO
            {
                IdEntidad = model.Vendedor.IdEntidad
            };

            var datosVendedor = new VendedorLogica().ObtenerVendedorPorCodigo(vendedorDTO);

            HttpContext.Session.SetString("PuestoVendedor", ((VendedorDTO)datosVendedor).Puesto);

            // return View("Puesto Vendedor Seleccionado");
            return RedirectToAction("DetailsVendedor", new { id = model.Vendedor.IdEntidad });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
