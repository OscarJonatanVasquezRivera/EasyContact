using DAL;
using EL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ContactosBLL
    {
        private ContactosDAL _contactosDAL;

        public int Guardar(ContactosEL contacto, int id = 0, bool esEdicion = false)
        {
            _contactosDAL = new ContactosDAL();

            contacto.Nombres = contacto.Nombres.Trim();
            contacto.Telefono = contacto.Telefono.Trim().Replace("-", "").Replace("(", "").Replace(")", "");
            contacto.Correo = contacto.Correo.Trim();
            contacto.Direccion = contacto.Direccion?.Trim();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(contacto.Nombres))
                throw new Exception("El nombre del contacto no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contacto.Telefono))
                throw new Exception("El teléfono del contacto no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contacto.Correo))
                throw new Exception("El correo del contacto no puede estar vacío.");

            return _contactosDAL.Guardar(contacto, id, esEdicion);
        }

        public int Actualizar(ContactosEL contacto)
        {
            _contactosDAL = new ContactosDAL();
            return _contactosDAL.Actualizar(contacto);
        }

        public List<ContactosEL> Listar(int idUsuario)
        {
            _contactosDAL = new ContactosDAL();
            return _contactosDAL.MostrarContactos(idUsuario);
        }

        public ContactosEL BuscarPorId(int id, int idUsuario)
        {
            var contactos = Listar(idUsuario);
            return contactos.Where(x => x.IdContacto == id).FirstOrDefault();
        }

        public List<ContactosEL> BuscarPorNombre(string nombre, int idUsuario)
        {
            var contactos = Listar(idUsuario);
            return contactos
                .Where(x => x.Nombres.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public int Eliminar(int id, int idUsuario)
        {
            _contactosDAL = new ContactosDAL();
            return _contactosDAL.Eliminar(id, idUsuario);
        }

        public ContactosEL Editar(int id, int idUsuario)
        {
            return BuscarPorId(id, idUsuario);
        }
    }
}
