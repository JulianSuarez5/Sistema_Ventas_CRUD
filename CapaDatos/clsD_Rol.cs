using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class clsD_Rol
    {
        public List<clsRol> Listar()
        {
            List<clsRol> Lista = new List<clsRol>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdRol, Descripcion FROM Rol");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsRol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsRol>();
                }
                return Lista;
            }
        }
    }
}
