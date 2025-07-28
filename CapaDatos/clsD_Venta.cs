using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class clsD_Venta
    {
        #region[Método para obtener el ID consecutivo de la venta]
        public int ObtenerConsecutivo()
        {
            int IdConsecutivo = 0;


            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT (*) +1 FROM VENTA");
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
        #endregion

        #region[Método para ir restando el stock despues de una venta]
        public bool RestarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE PRODUCTO SET Stock = stock - @cantidad WHERE idproducto = @idproducto");
                    SqlCommand comando = new SqlCommand (query.ToString(), Conexion);
                    comando.Parameters.AddWithValue("@cantidad", cantidad);
                    comando.Parameters.AddWithValue("@idproducto", idproducto);
                    comando.CommandType = CommandType.Text;

                    Conexion.Open();

                    respuesta = comando.ExecuteNonQuery() > 0 ? true : false; 
                }
                catch (Exception)
                {

                    respuesta = false;
                }
            }
            return respuesta;
        }
        #endregion

        #region[Método para sumar stock en caso de que se requiera devolver unidades al inventario ]
        public bool SumarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE PRODUCTO SET Stock = stock + @cantidad WHERE idproducto = @idproducto");
                    SqlCommand comando = new SqlCommand(query.ToString(), Conexion);
                    comando.Parameters.AddWithValue("@cantidad", cantidad);
                    comando.Parameters.AddWithValue("@idproducto", idproducto);
                    comando.CommandType = CommandType.Text;

                    Conexion.Open();

                    respuesta = comando.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception)
                {

                    respuesta = false;
                }
            }
            return respuesta;
        }
        #endregion
        
        #region[Método para registrar las ventas]
        public bool Registrar(clsVenta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_VENTA".ToString(), Conexion);
                    Comando.Parameters.AddWithValue("IdUsuario", obj.objUsuario.IdUsuario);
                    Comando.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    Comando.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    Comando.Parameters.AddWithValue("DocumentoCliente", obj.DocumentoCliente);
                    Comando.Parameters.AddWithValue("NombreCliente", obj.NombreCliente);
                    Comando.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    Comando.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    Comando.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    Comando.Parameters.AddWithValue("DetalleVenta", DetalleVenta);

                    Comando.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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
        #endregion

        #region[Método para recuperar los detalles completos de una venta específica]
        public clsVenta ObtenerVenta(string numero)
        {
            clsVenta obj = new clsVenta();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    Conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.IdVenta, u.NombreCompleto,");
                    query.AppendLine("v.DocumentoCliente, v.NombreCliente,");
                    query.AppendLine("v.TipoDocumento, v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago, v.MontoCambio, v.MontoTotal,");
                    query.AppendLine("CONVERT(CHAR(10), v.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("INNER JOIN USUARIO u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("WHERE v.NumeroDocumento = @numero");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.Parameters.AddWithValue("numero", numero);
                    Comando.CommandType = CommandType.Text;

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new clsVenta() 
                            {
                                IdVenta = int.Parse(dr["IdVenta"].ToString()),
                                objUsuario = new clsUsuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception)
                {

                    obj = new clsVenta();
                }
            }
                return obj;
        }
        #endregion

        #region[Método para recuperar los detalles específicos de los productos vendidos en una venta determinada]
        public List<clsDetalle_Venta> ObtenerDetalleVenta(int idventa)
        {
            List<clsDetalle_Venta> objLista = new List<clsDetalle_Venta>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    Conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.Nombre, dv.Precio_Venta, dv.Cantidad,dv.SubTotal ");
                    query.AppendLine("FROM DETALLE_VENTA dv ");
                    query.AppendLine("INNER JOIN PRODUCTO p on p.IdProducto = dv.IdProducto");
                    query.AppendLine("WHERE dv.IdVenta = @idventa");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.Parameters.AddWithValue("idventa", idventa);
                    Comando.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objLista.Add(new clsDetalle_Venta()
                                    {
                                        oProducto = new clsProducto() { Nombre = dr["Nombre"].ToString() },
                                        PrecioVenta = Convert.ToDecimal(dr["Precio_Venta"].ToString()),
                                        Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                        SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString())
                                    });
                        }
                    }


                }
                catch (Exception)
                {

                    objLista = new List<clsDetalle_Venta>();
                }
            }

                return objLista;
        }
        #endregion
    }
}