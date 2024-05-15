using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class clsD_Compra
    {
        public int ObtenerConsecutivo()
        {
            int IdConsecutivo = 0;


            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT (*) +1 FROM COMPRA");
                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    IdConsecutivo = Convert.ToInt32(Comando.ExecuteScalar());

                }
                catch (Exception)
                {
                    IdConsecutivo = 0;
                }
            }

            return IdConsecutivo;
        }

        public bool RegistrarCompra(clsCompra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_COMPRA".ToString(), Conexion);
                    Comando.Parameters.AddWithValue("IdUsuario", obj.objUsuario.IdUsuario);
                    Comando.Parameters.AddWithValue("IdProveedor", obj.objProveedor.IdProveedor);
                    Comando.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    Comando.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    Comando.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    Comando.Parameters.AddWithValue("DetalleCompra", DetalleCompra);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Comando.CommandType = CommandType.StoredProcedure;

                    Conexion.Open();

                    Comando.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();

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
}
