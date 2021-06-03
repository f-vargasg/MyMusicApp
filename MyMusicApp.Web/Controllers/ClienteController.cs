using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusicApp.Web.ViewModel;
using MyMusicApp.Web.Helpers;
using MyMusicApp.Logica;
using MyMusicApp.DTO;

namespace MyMusicApp.Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController
        public ActionResult Index()
        {
            SucursalProductoVM model = new SucursalProductoVM();

            Random aleatorio = new Random();
            int resultadoAleatorio = aleatorio.Next(1, 10);

            if (resultadoAleatorio <= 5)
            {
                model.Sucursal = new DTO.SucursalDTO
                {
                    DirSucursal = "Heredia",
                    HorarioSucursal = "08-17"

                };

                model.Producto = new DTO.ProductoDTO
                {
                    NombreProducto = "Guitarra",
                    CantidadInventario = 5,
                    PrecioUnitario = 10000,
                    TipoProducto = 1
                };

                model.NombreTipo = Enum.GetName(typeof(TiposProducto), model.Producto.TipoProducto);
            }
            else
            {
                model.Error = new DTO.ErrorDTO
                {
                    MensajeError = "No fue posible cargar la información, el resultado aleatorio fue " +
                                   resultadoAleatorio + "."
                };
            }
            

            return View(model);
        }


        // GET: ClienteController/ListarProductosPorTipo?idProducto=5
        public ActionResult ListarProductosPorTipo(int idProducto)
        {
            SucursalProductoVM model = new SucursalProductoVM();
            var resultado = new ClaseEjemploLogica().ObtenerProductoPorTipo(idProducto);   // Cambiarlo por ListarProductosPorTipo
            if (resultado.ElementAt(0).GetType() == typeof(ErrorDTO))
            {
                model.Error = (ErrorDTO)resultado.ElementAt(0);
            }
            else
            {
                model.ListadoProductos = new List<ProductoDTO>();
                foreach (var item in resultado)
                {
                    model.ListadoProductos.Add((ProductoDTO)item);
                }
            }
            return View(model);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            SucursalProductoVM model = new SucursalProductoVM();

            var resultado = new SucursalLogica().ObtenerSucursalPorCodigo(id);

            if (resultado.GetType() == typeof (ErrorDTO))
            {
                model.Error = (ErrorDTO)resultado;
            }
            else
            {
                model.Sucursal = (SucursalDTO)resultado;
            }
            return View(model);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SucursalDTO model)
        {
            try
            {
                var resultado = new SucursalLogica();   // TODO: Hacerlo con un AgregarSucursal (SucursalDTO)
                resultado.AgregarSucursal(model.DirSucursal, model.HorarioSucursal, model.TelefonoSucursal, 
                                            model.CorreoElectronico);
                if (resultado.GetType() == typeof(ErrorDTO))
                {
                    throw new Exception("Error");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/CreateCliente
        public ActionResult CreateCliente()
        {
            return View();
        }

        // POST: ClienteController/CreateCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCliente(ClienteDTO model)
        {
            try
            {
                var resultado = new ClienteLogica().AgregarCliente(model);   


                if (resultado.GetType() == typeof(ErrorDTO))
                {
                    throw new Exception("Error");
                }
                else
                {
                    return RedirectToAction("DetailsCliente", new { id = resultado.IdEntidad });
                    // return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult DetailsCliente(int id)
        {
            SucursalProductoVM model = new SucursalProductoVM();

            var resultado = new ClienteLogica().ObtenerClientePorCodigo(id);

            if (resultado.GetType() == typeof(ErrorDTO))
            {
                model.Error = (ErrorDTO)resultado;
            }
            else
            {
                model.Cliente = (ClienteDTO)resultado;
            }
            return View(model);
        }


        public ActionResult Error(string mensajeError)
        {
            return View();
        }


        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
