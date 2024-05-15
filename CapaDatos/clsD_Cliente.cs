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
    public class clsD_Cliente
    {
        public List<clsCliente> Listar()
        {
            List<clsCliente> Lista = new List<clsCliente>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdCliente,Documento,NombreCompleto,Correo,Telefono,Estado FROM CLIENTE");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsCliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsCliente>();
                }
                return Lista;
            }
        }
        public int Registrar(clsCliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdClienteGenerado = 0;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_CLIENTE", Conexion);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    Comando.Parameters.AddWithValue("Correo", obj.Correo);
                    Comando.Parameters.AddWithValue("Telefono", obj.Telefono);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    IdClienteGenerado = Convert.ToInt32(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                IdClienteGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdClienteGenerado;
        }

        public bool Actualizar(clsCliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ACTUALIZAR_CLIENTE", Conexion);
                    Comando.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
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

        public bool Eliminar(clsCliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("DELETE FROM CLIENTE WHERE IdCliente = @id", Conexion);
                    Comando.Parameters.AddWithValue("id", obj.IdCliente);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    Respuesta = Comando.ExecuteNonQuery() > 0 ? true : false;
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
