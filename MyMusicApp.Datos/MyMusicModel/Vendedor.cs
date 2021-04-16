﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            OrdenCompras = new HashSet<OrdenCompra>();
        }

        public int PkVendedor { get; set; }
        public string CodCedula { get; set; }
        public string NomVendedor { get; set; }
        public string DesPuesto { get; set; }
        public int? IdSucursal { get; set; }
        public int FkSucursal { get; set; }

        public virtual Sucursal FkSucursalNavigation { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompras { get; set; }
    }
}