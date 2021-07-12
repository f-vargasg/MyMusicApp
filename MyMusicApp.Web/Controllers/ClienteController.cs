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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MyMusicApp.Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController
        public ActionResult Index()
        {

            SessionHelper.VerificadorUsuario(HttpContext.Session);

            SucursalProductoVM model = new SucursalProductoVM();

            Random aleatorio = new Random();
            int resultadoAleatorio = aleatorio.Next(1, 10);

            if (resultadoAleatorio <= 5)
            {
                model.Sucursal = new DTO.SucursalDTO
                {
                    DirSucursal = "Heredia",
                    HorarioSucursal = "08-17",
                    Mensaje = HttpContext.Session.GetString("Usuario")
                };

                model.Producto = new DTO.ProductoDTO
                {
                    NombreProducto = "Guitarra",
                    CantidadInventario = 5,
                    PrecioUnitario = 10000,
                    TipoProducto = 1
                };

                model.NombreTipo = Enum.GetName(typeof(TiposProducto), model.Producto.TipoProducto);
                ViewBag.Usuario = "El usuario conectado es " + HttpContext.Session.GetString("Usuario");
                ViewBag.TiempoConexion = "El tiempo de conexión es " + DateTime.Now.ToString();
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

            SessionHelper.VerificadorUsuario(HttpContext.Session);

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
                // model.Sucursal = new SucursalDTO { Mensaje = HttpContext.Session.GetString("Usuario") };
                ViewData["Usuario"] = "El archivo conectado es: " + HttpContext.Session.GetString("Usuario");
            }
            return View(model);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            SucursalProductoVM model = new SucursalProductoVM();

            var resultado = new SucursalLogica().ObtenerSucursalPorCodigo(id);

            if (resultado.GetType() == typeof(ErrorDTO))
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
                var resultado = new SucursalLogica().AgregarSucursal(model.DirSucursal, model.HorarioSucursal, model.TelefonoSucursal,
                                            model.CorreoElectronico);  // TODO: Hacerlo con el parametro sucursaldDTO
                if (resultado.GetType() == typeof(ErrorDTO))
                {
                    throw new Exception("Error");
                }
                else
                {
                    return RedirectToAction("Details", new { id = resultado.IdEntidad });
                    // return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }
        }
        //------------------------------------------------------------------------
        // Metodos para la entidad cliente 
        //------------------------------------------------------------------------
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


        //GET: ClienteController/ListarSucursales
        public ActionResult ListarSucursales()
        {
            SucursalProductoVM model = new SucursalProductoVM();

            var resultado = new SucursalLogica().ListarTotalSucursales();

            if (resultado.ElementAt(0).GetType() == typeof(ErrorDTO))
            {
                model.Error = (ErrorDTO)resultado.ElementAt(0);
            }
            else
            {
                model.ListadoSucursales = new List<SucursalDTO>();
                foreach (var item in resultado)
                {
                    var itemConvertido = (SucursalDTO)item;
                    model.ListadoSucursales.Add(itemConvertido);
                }
            }
            return View(model);
        }

        public ActionResult Error(string mensajeError)
        {
            return View();
        }

        public ActionResult ListarProductos()
        {
            SucursalProductoVM model = new SucursalProductoVM();
            var resultado = new ProductoLogica().ListarProductos();

            if (resultado.ElementAt(0).GetType() == typeof(ErrorDTO))
            {
                // mensaje de erorr
                model.Error = (ErrorDTO)resultado.ElementAt(0);
            }
            else
            {
                model.ListadoProductos = new List<ProductoDTO>();
                foreach (var item in resultado)
                {
                    model.ListadoProductos.Add((ProductoDTO)item);
                }
                // datos correctos
            }
            return View(model);

        }

        [Route("Productos")]
        [ResponseCache(Location = ResponseCacheLocation.None)]
        public ActionResult Productos()
        {
            SucursalProductoVM model = new SucursalProductoVM();
            var resultado = new ProductoLogica().ListarProductos();

            if (resultado.ElementAt(0).GetType() == typeof(ErrorDTO))
            {
                // mensaje de erorr
                model.Error = (ErrorDTO)resultado.ElementAt(0);
            }
            else
            {
                model.ListadoProductos = new List<ProductoDTO>();
                foreach (var item in resultado)
                {
                    model.ListadoProductos.Add((ProductoDTO)item);
                }
                // datos correctos
            }
            return Json(model.ListadoProductos);

        }
        public ActionResult DetailsProducto(int id)
        {
            var resultado = new ProductoLogica().ObtenerProductoPorCodigo(id);

            CarritoComprasVM model = new CarritoComprasVM();
            model.ProductoVista = (ProductoDTO)resultado;

            // Esto para pruebas se ocupa bajar a la BL
            // HttpContext.Session.SetString("ProdRelacionado", "Producto Prueba Relacionado");
            var prodRelacionado = new ProductoLogica().ListarProductoDeSegundaParecidosA(model.ProductoVista.NombreProducto);
            ViewBag.ProdRelacionado = "No Existe producto Relacionado";
            if (prodRelacionado.GetType() != typeof(ErrorDTO))
            {
                ViewBag.ProdRelacionado = "El nombre del producto de segunda relacionado es: " + ((ProductoDTO)prodRelacionado).NombreProducto; ;
            }
            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsProducto(CarritoComprasVM model)
        {
            if (HttpContext.Session.GetInt32("CantidadProductos") != null)
            {
                if (HttpContext.Session.GetInt32("CantidadProductos") >= 0)
                {
                    int? contadorProductos = HttpContext.Session.GetInt32("CantidadProductos");
                    HttpContext.Session.SetInt32("CantidadProductos", Convert.ToInt32(contadorProductos + 1));
                    HttpContext.Session.SetInt32("Producto" + Convert.ToInt32(contadorProductos + 1), model.ProductoVista.IdEntidad);
                    HttpContext.Session.SetInt32("CantidadProducto" + Convert.ToInt32(contadorProductos + 1), model.CantidadxProducto);
                    model.ProductoVista.Mensaje = "Producto agregado al carrito! Hay " + HttpContext.Session.GetInt32("CantidadProductos") +
                                                  " productos en el carrito";
                    return View(model);
                }
                else
                {
                    return Content("Sección de codigo inalcanzable");
                }
            }
            else
            {
                HttpContext.Session.SetInt32("CantidadProductos", 1);
                HttpContext.Session.SetInt32("Producto1", model.ProductoVista.IdEntidad);
                HttpContext.Session.SetInt32("CantidadProducto1", model.CantidadxProducto);
                model.ProductoVista.Mensaje = "Producto agregado al carrito! Hay " + HttpContext.Session.GetInt32("CantidadProductos") +
                                              " productos en el carrito";
                return View(model);
            }
        }

        public ActionResult ContenidoCarritoCompras()
        {
            CarritoComprasVM model = new CarritoComprasVM();
            var cantidad = HttpContext.Session.GetInt32("CantidadProductos");

            if (cantidad > 0)
            {
                ViewBag.CantidadProductosCarrito = cantidad;

                model.ListaProductosCarrito = new List<ProductoDTO>();
                model.ListaCantidadPorProducto = new List<int>();
                for (int i = 1; i <= cantidad; i++)
                {
                    var datoProducto = new ProductoLogica().ObtenerProductoPorCodigo(Convert.ToInt32(HttpContext.Session.GetInt32("Producto" + i)));
                    model.ListaProductosCarrito.Add((ProductoDTO)datoProducto);
                    model.ListaCantidadPorProducto.Add(Convert.ToInt32(HttpContext.Session.GetInt32("CantidadProducto" + i)));
                }

                ViewBag.MontoTotal = model.ListaProductosCarrito.Sum(P => P.PrecioUnitario);
            }
            else
            {
                ViewBag.Vacio = "No hay productos que mostrar en el carrito";
            }

            return View(model);
        }

        public ActionResult EliminarProducto(int id)
        {
            HttpContext.Session.Remove("Producto" + id);
            HttpContext.Session.Remove("CantidadProducto" + id);
            int? contadorProductos = HttpContext.Session.GetInt32("CantidadProductos");
            HttpContext.Session.SetInt32("CantidadProductos", Convert.ToInt32(contadorProductos - 1));

            return RedirectToAction("ContenidoCarritoCompras");
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

        public ActionResult ListarProductosSegunda()
        {
            SucursalProductoVM model = new SucursalProductoVM();
            var resultado = new ProductoLogica().ListarProductosDeSegunda();
            /*
            var url = "https://localhost:44310/api/MyMusicAppServices/GetProductosSegunda";

            var webrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            string datos = "";
            using (var response = webrequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var resultadoLectura = reader.ReadToEnd();
                    datos = resultadoLectura.ToString();
                }
            }

            var resultado = JsonConvert.DeserializeObject<List<ProductoDTO>>(datos);
            */

            model.ListadoProductos = new List<ProductoDTO>();
            foreach (var item in resultado)
            {
                model.ListadoProductos.Add((ProductoDTO)item);
            }
            // datos correctos

            return View(model);

        }

        public ActionResult EjemploReact()
        {
            return View();
        }

    }
}
