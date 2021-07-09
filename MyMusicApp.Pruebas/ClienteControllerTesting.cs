using System;
using Xunit;
using MyMusicApp.Web.Controllers;
using MyMusicApp.Web.ViewModel;
using MyMusicApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MyMusicApp.Pruebas
{
    public class ClienteControllerTesting
    {
        private ClienteController _controller;
        public ClienteControllerTesting()
        {
            _controller = new ClienteController();
        }

        [Fact]
        public void Testing_Cliente_Index()
        {
            var metodoDelControlador = _controller.Index();
            var vista = Assert.IsType<ViewResult>(metodoDelControlador);

            var model = Assert.IsType<SucursalProductoVM>(vista.Model);
            Assert.True(model.Error != null);
        }

        [Fact]
        public void Testing_Cliente_Create()
        {
            SucursalDTO sucursalDTO = new SucursalDTO
            {
                CorreoElectronico = "",
                TelefonoSucursal = "342424234",
                HorarioSucursal = "19:00-20:00",
                DirSucursal = "Guanacaste"
            };

            var metodo = _controller.Create(sucursalDTO);
            Assert.IsType<RedirectToActionResult>(metodo);
        }
    }
}
