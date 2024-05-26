using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class clsConexion
    {
        public static string Cadena = ConfigurationManager.ConnectionStrings["Cadena_Conexion"].ToString();
    }
}