using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApp.Web.ViewModel
{
    public class VendedorVM
    {
        public VendedorDTO Vendedor { get; set; }
        public ErrorDTO Error { get; set; }
    }
}
