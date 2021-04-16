using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Productos = new HashSet<Producto>();
            Vendedors = new HashSet<Vendedor>();
        }

        public int PkSucursal { get; set; }
        public string DirUbicacion { get; set; }
        public string DesHorario { get; set; }
        public string TelSucursal { get; set; }
        public string EmSucursal { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Vendedor> Vendedors { get; set; }
    }
}
