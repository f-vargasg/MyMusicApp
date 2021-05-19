using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Logica
{
    public class SucursalLogica
    {
        #region Variables
        #endregion

        #region Constructores
        #endregion

        #region Metodos
        #region Conversiones
        internal static SucursalDTO ConvertirDatosSucursalADTO(Sucursal sucursal)
        {
            return new SucursalDTO
            {
                CorreoElectronico = sucursal.EmlSucursal,
                DirSucursal = sucursal.DirUbicacion,
                HorarioSucursal = sucursal.DesHorario,
                IdEntidad = sucursal.PkSucursal,
                TelefonoSucursal = sucursal.TelSucursal
            };
        }

        internal static Sucursal ConvertirSucursalDTOaDatos(SucursalDTO sucursalDTO)
        {
            return new Sucursal
            {
                DesHorario = sucursalDTO.HorarioSucursal,
                DirUbicacion = sucursalDTO.DirSucursal,
                EmlSucursal = sucursalDTO.CorreoElectronico,
                TelSucursal = sucursalDTO.TelefonoSucursal
            };
        }

        #endregion

        #region Funcion
        #endregion
        #endregion
    }
}
