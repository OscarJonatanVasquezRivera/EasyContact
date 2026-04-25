using System;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{

    // Clase estática para almacenar información de sesión 
    //mientras la aplicación esté en ejecución. Esta clase permite acceder a
    //los datos del usuario que ha iniciado sesión desde cualquier parte de la aplicación.
    //sin tener que instanciar la clase Sesion.

    public static class Sesion
    {
        public static int IdUsuario { get; set; }
        public static string Usuario { get; set; }
        public static string Rol { get; set; }
    }
}
