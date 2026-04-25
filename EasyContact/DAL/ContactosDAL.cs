using EL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactosDAL
    {
        // Guardar: sirve para insertar o actualizar
        public int Guardar(ContactosEL contacto, int id = 0, bool esEdicion = false)
        {
            using (SqlConnection cn = new ConexionDB().ObtenerConexion())
            {
                cn.Open();

                string query;

                if (esEdicion)
                {
                    query = "UPDATE ContactosEL SET Nombres=@nombres, Telefono=@telefono, Correo=@correo, Direccion=@direccion " +
                            "WHERE IdContacto=@idContacto AND IdUsuario=@idUsuario";
                }
                else
                {
                    query = "INSERT INTO ContactosEL (Nombres, Telefono, Correo, Direccion, IdUsuario) " +
                            "VALUES (@nombres, @telefono, @correo, @direccion, @idusuario)";
                }

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@nombres", contacto.Nombres);
                cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("@correo", contacto.Correo);
                cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
                cmd.Parameters.AddWithValue("@idusuario", contacto.IdUsuario);

                if (esEdicion)
                {
                    cmd.Parameters.AddWithValue("@idContacto", contacto.IdContacto);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        // Mostrar contactos de un usuario
        public List<ContactosEL> MostrarContactos(int idUsuario)
        {
            List<ContactosEL> lista = new List<ContactosEL>();

            using (SqlConnection cn = new ConexionDB().ObtenerConexion())
            {
                cn.Open();

                string query = "SELECT * FROM ContactosEL WHERE IdUsuario=@idUsuario";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ContactosEL
                    {
                        IdContacto = Convert.ToInt32(dr["IdContacto"]),
                        Nombres = dr["Nombres"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"])
                    });
                }
            }

            return lista;
        }

        // Actualizar (separado, aunque Guardar con esEdicion=true también lo hace)
        public int Actualizar(ContactosEL contacto)
        {
            using (SqlConnection cn = new ConexionDB().ObtenerConexion())
            {
                cn.Open();

                string query = "UPDATE ContactosEL SET Nombres=@nombres, Telefono=@telefono, Correo=@correo, Direccion=@direccion " +
                               "WHERE IdContacto=@idContacto AND IdUsuario=@idUsuario";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@nombres", contacto.Nombres);
                cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("@correo", contacto.Correo);
                cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
                cmd.Parameters.AddWithValue("@idContacto", contacto.IdContacto);
                cmd.Parameters.AddWithValue("@idUsuario", contacto.IdUsuario);

                return cmd.ExecuteNonQuery();
            }
        }

        // Eliminar
        public int Eliminar(int idContacto, int idUsuario)
        {
            using (SqlConnection cn = new ConexionDB().ObtenerConexion())
            {
                cn.Open();

                string query = "DELETE FROM ContactosEL WHERE IdContacto=@idContacto AND IdUsuario=@idUsuario";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idContacto", idContacto);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}

