﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security;

namespace CapaDatos
{
    public class clsConexion
    {
        public static readonly string Cadena = ConfigurationManager.ConnectionStrings["Cadena_Conexion"].ToString();
    }
}
