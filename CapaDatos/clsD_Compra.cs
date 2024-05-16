using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            }
            return Respuesta;
        } 
        
        public clsCompra ObtenerCompra(string numero)
        {
            clsCompra obj = new clsCompra();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT C.IdCompra,");
                    query.AppendLine("U.NombreCompleto,");
                    query.AppendLine("pr.Documento, pr.RazonSocial,");
                    query.AppendLine("C.TipoDocumento, C.NumeroDocumento, C.MontoTotal, CONVERT(CHAR(10), C.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("FROM COMPRA C");
                    query.AppendLine("INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario");
                    query.AppendLine("INNER JOIN PROVEEDOR pr ON pr.IdProveedor = C.IdProveedor");
                    query.AppendLine("WHERE C.NumeroDocumento = @numero");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.Parameters.AddWithValue("numero", numero);

                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new clsCompra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                objUsuario = new clsUsuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                objProveedor = new clsProveedor() {Documento = dr["Documento"].ToString(),RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }

                }
                catch (Exception)
                {

                    obj = new clsCompra();
                }
            }

            return obj;
        }

        public List<clsDetalle_Compra> ObtenerDetalle(int idcompra)
        {
            List<clsDetalle_Compra> objLista = new List<clsDetalle_Compra>();

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    Conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT p.Nombre, dc.Precio_Compra, dc.Cantidad, dc.MontoTotal FROM DETALLE_COMPRA dc");
                    query.AppendLine("INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto");
                    query.AppendLine("WHERE dc.IdCompra = @idcompra");

                    SqlCommand comando = new SqlCommand(query.ToString(), Conexion);
                    comando.Parameters.AddWithValue("@idcompra", idcompra);
                    comando.CommandType = System.Data.CommandType.Text;

                    using(SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objLista.Add(new clsDetalle_Compra()
                            {
                                objProducto = new clsProducto() { Nombre = dr["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(dr["Precio_Compra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString())          
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

                objLista = new List<clsDetalle_Compra>();
            }
            return objLista;
        }
    }
}
