using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMusicApp.DTO;

namespace MyMusicApp.Web.ViewModel
{
    public class SucursalProductoVM
    {
        public SucursalDTO Sucursal { get; set; }

        public ProductoDTO Producto { get; set; }

        public ClienteDTO Cliente { get; set; }

        public List<ProductoDTO> ListadoProductos { get; set; }

        public List<SucursalDTO> ListadoSucursales { get; set; }

        public ErrorDTO Error { get; set; }

        public SelectList Tipos { get; set; }

        public int TipoSeleccionado { get; set; }

        public string NombreTipo { get; set; }
    }
}
