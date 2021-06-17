using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApp.Web.Helpers
{
    public static class SessionHelper
    {
        public static void VerificadorUsuario (ISession session)
        {
            if (session.GetString ("Usuario") == null )
            {
                session.SetString("Usuario", "Antonio");
            }
        }
    }
}
