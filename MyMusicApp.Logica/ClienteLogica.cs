using MyMusicApp.Datos.MyMusicModel;
using MyMusicApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicApp.Logica
{
    public class ClienteLogica
    {
        internal static ClienteDTO ConvertirDatosClienteADTO (Cliente cliente)
        {
            return new ClienteDTO
            {
                IdEntidad = cliente.PkCliente,
                Email = cliente.EmlDirCliente,
                FechaNacimiento = Convert.ToDateTime(cliente.FecNacimiento),
                IdCedula = cliente.DesCedula,
                IdContrasena = cliente.DesContrasena,
                Nombre = cliente.NomCliente,
                Sexo = cliente.TipSexo,
                Telefono = cliente.TelCliente,
                UsuarioCliente = cliente.UsrCliente
            };
        }
        internal static Cliente ClienteDTOADatos (ClienteDTO clienteDTO)
        {
            return new Cliente
            {
                DesCedula = clienteDTO.IdCedula,
                DesContrasena = clienteDTO.IdContrasena,
                EmlDirCliente =clienteDTO.Email,
                FecNacimiento =clienteDTO.FechaNacimiento,
                NomCliente = clienteDTO.Nombre,
                TelCliente = clienteDTO.Telefono,
                TipSexo = clienteDTO.Sexo,
                UsrCliente = clienteDTO.UsuarioCliente
                
            };
        }
    }
}
