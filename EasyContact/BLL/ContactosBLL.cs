using DAL;
using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class ContactosBLL
    {
        private ContactosDAL _contactosDAL;

        public int Guardar(ContactosEL contacto, int id = 0, bool esEdicion = false)
        {
            _contactosDAL = new ContactosDAL();

            //Se valida el correo electrónico antes de guardar el contacto
            contacto.Nombres = contacto.Nombres.Trim();
            contacto.Telefono = contacto.Telefono.Trim().Replace("-", "").Replace("(", "").Replace(")", "");
            contacto.Correo = contacto.Correo.Trim();
            contacto.Direccion = contacto.Direccion?.Trim();

            //se valida usando isnullorempty para verificar si el campo de correo electrónico está vacío o nulo
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(contacto.Nombres))
                throw new Exception("El nombre del contacto no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contacto.Telefono))
                throw new Exception("El teléfono del contacto no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contacto.Correo))
                throw new Exception("El correo del contacto no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contacto.Direccion))
                throw new Exception("La direccion del contacto no puede estar vacía.");

            //Se llama la F de valida correo
            ValidarCorreo(contacto.Correo);

            //Se llama la F de valida duplicados
            ValidarDuplicados(contacto, contacto.IdUsuario);

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

        // se valida el formato del correo electrónico utilizando una expresión regular
        //regex es una funcion que permite validar el formato de un correo electrónico
        //mediante una expresión regular que verifica si el correo electrónico cumple con el
        //formato estándar de un correo electrónico válido.
        public void ValidarCorreo(string correo)
        {
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("El formato del correo no es válido.");
        }

        //Se valida que no no se pueda ingresar un contacto con el mismo número de teléfono
        //se deja solamente el número de teléfono como campo único para evitar duplicados
        //y asi que una persona con sus mismo correo pueda ingresar varios contactos con el mismo correo
        //pero con diferente número de teléfono
        public void ValidarDuplicados(ContactosEL contacto, int idUsuario)
        {
            var lista = Listar(idUsuario);
            var existente = lista.FirstOrDefault(i =>
        i.Telefono == contacto.Telefono && i.IdContacto != contacto.IdContacto);

            if (existente != null)
                throw new Exception("Ya existe un contacto con ese teléfono.");
        }


    }
}
