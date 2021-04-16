using System;
using System.Collections.Generic;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class Cliente
    {
        public Cliente()
        {
            OrdenCompras = new HashSet<OrdenCompra>();
        }

        public int PkCliente { get; set; }
        public string DesCedula { get; set; }
        public string NomCliente { get; set; }
        public string TipSexo { get; set; }
        public DateTime? FecNacimiento { get; set; }
        public string UsrCliente { get; set; }
        public string DesContrasena { get; set; }
        public string TelCliente { get; set; }
        public string EmlDirCliente { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompras { get; set; }
    }
}
