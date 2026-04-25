using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConexionDB
    {
        private readonly string _conexiondb = "Server=JONATAN_RIVERA;Database=EasyContactDB;Trusted_Connection=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_conexiondb);
        }
    }

}
