using System;
using EL;
using DAL;

namespace BLL
{
    public class UsuariosBLL
    {
        private UsuariosDAL _usuariosDAL;

 
        public UsuariosEL Login(string usuario, string clave)
        {
            _usuariosDAL = new UsuariosDAL();

        
            usuario = usuario.Trim();
            clave = clave.Trim();

      
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("El usuario no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("La clave no puede estar vacía.");

         
            return _usuariosDAL.Login(usuario, clave);
        }
    }
}
