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
    public class clsD_Proveedor
    {
        public List<clsProveedor> Listar()
        {
            List<clsProveedor> Lista = new List<clsProveedor>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdProveedor,Documento,RazonSocial,Correo, Telefono,Estado FROM PROVEEDOR");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsProveedor()
                            {
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Documento = dr["Documento"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsProveedor>();
                }
                return Lista;
            }
        }
        public int Registrar(clsProveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdProveedorGenerado = 0;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_PROVEEDOR", Conexion);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("RazonSocial", obj.RazonSocial);
                    Comando.Parameters.AddWithValue("Correo", obj.Correo);
                    Comando.Parameters.AddWithValue("Telefono", obj.Telefono);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    IdProveedorGenerado = Convert.ToInt32(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                IdProveedorGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdProveedorGenerado;
        }

        public bool Actualizar(clsProveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ACTUALIZAR_PROVEEDOR", Conexion);
                    Comando.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("RazonSocial", obj.RazonSocial);
                    Comando.Parameters.AddWithValue("Correo", obj.Correo);
                    Comando.Parameters.AddWithValue("Telefono", obj.Telefono);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }

        public bool Eliminar(clsProveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ELIMINAR_PROVEEDOR", Conexion);
                    Comando.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }
    }
}
