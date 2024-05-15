using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class clsD_Permiso
    {
        public List<clsPermiso> Listar(int IdUsuario)
        {
            List<clsPermiso> Lista = new List<clsPermiso>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdRol,p.NombreMenu FROM Permiso P");
                    query.AppendLine("INNER JOIN ROL r ON r.IdRol = p.IdRol");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IdRol = r.IdRol");
                    query.AppendLine("WHERE u.IdUsuario = @IdUsuario");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsPermiso()
                            {
                                objRol = new clsRol() { IdRol = Convert.ToInt32(dr["idRol"])},
                                NombreMenu = dr["NombreMenu"].ToString(),

                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsPermiso>();
                }
                return Lista;
            }
        }
    }
}
