using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class clsD_Negocio
    {
        public clsNegocio ObtenerDatos() 
        {
            clsNegocio obj = new clsNegocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(clsConexion.Cadena))
                {
                    conexion.Open();

                    string query = "SELECT IdNegocio,Nombre,RUC,Direccion FROM NEGOCIO WHERE IdNegocio = 1";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.CommandType = CommandType.Text;

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new clsNegocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {

                obj = new clsNegocio();
            }

            return obj;
        }

        public bool GuardarDatos(clsNegocio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(clsConexion.Cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE NEGOCIO SET Nombre = @nombre,");
                    query.AppendLine("RUC = @ruc,");
                    query.AppendLine("Direccion = @direccion");
                    query.AppendLine("WHERE IdNegocio = 1");

                    SqlCommand comando = new SqlCommand(query.ToString(), conexion);
                    comando.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    comando.Parameters.AddWithValue("@ruc", objeto.RUC);
                    comando.Parameters.AddWithValue("@direccion", objeto.Direccion);
                    comando.CommandType = CommandType.Text;

                    if (comando.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No sepudo guardar los datos";
                        respuesta = false;
                    }
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false ;
            }
            return respuesta;
        }

        public byte[] ObtenerLogo(out bool Obtenido)
        {
            Obtenido = true;
            byte[] LogoBytes = new byte[0];
            try
            {
                using (SqlConnection conexion = new SqlConnection(clsConexion.Cadena))
                {
                    conexion.Open();
                    string query = "SELECT Logo FROM NEGOCIO WHERE IdNegocio = 1";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.CommandType = CommandType.Text;

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LogoBytes = (Byte[])dr["Logo"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                Obtenido = false;
                LogoBytes = new byte[0];
            }
            return LogoBytes;
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(clsConexion.Cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE NEGOCIO SET Logo = @imagen");
                    query.AppendLine("WHERE IdNegocio = 1");

                    SqlCommand comando = new SqlCommand(query.ToString(), conexion);
                    comando.Parameters.AddWithValue("@imagen", image);
                    comando.CommandType = CommandType.Text;

                    if (comando.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No sepudo actualizar el logo";
                        respuesta = false;
                    }
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
