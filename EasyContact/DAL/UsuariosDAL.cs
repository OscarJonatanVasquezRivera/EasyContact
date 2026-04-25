using EL;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class UsuariosDAL
    {
        public UsuariosEL Login(string usuario, string clave)
        {
            using (SqlConnection cn = new ConexionDB().ObtenerConexion())
            {
                cn.Open();

                string query = "SELECT * FROM UsuariosEL WHERE Usuario=@usuario AND Clave=@clave";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new UsuariosEL
                    {
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Usuario = dr["Usuario"].ToString(),
                        Clave = dr["Clave"].ToString(),
                        Rol = dr["Rol"].ToString()
                    };
                }
            }

            // Si no se encontró usuario
            return null;
        }
    }
}﻿
