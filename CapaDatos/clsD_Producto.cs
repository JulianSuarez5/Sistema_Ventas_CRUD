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
    public class clsD_Producto
    {
        public List<clsProducto> Listar()
        {
            List<clsProducto> Lista = new List<clsProducto>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria, c.Descripcion[DescripcionCategoria], Stock, Precio_Compra, Precio_Venta, p.Estado FROM PRODUCTO P");
                    query.AppendLine("INNER JOIN CATEGORIA c on c.IdCategoria = p.IdCategoria");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsProducto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                objCategoria = new clsCategoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString()},
                                Stock = Convert.ToInt32(dr["Stock"].ToString()),
                                Precio_Compra = Convert.ToDecimal(dr["Precio_Compra"].ToString()),
                                Precio_Venta = Convert.ToDecimal(dr["Precio_Venta"].ToString()),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsProducto>();
                }
                return Lista;
            }
        }
        public int Registrar(clsProducto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdProductoGenerado = 0;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_PRODUCTO ", Conexion);
                    Comando.Parameters.AddWithValue("Codigo", obj.Codigo);
                    Comando.Parameters.AddWithValue("Nombre", obj.Nombre);
                    Comando.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    Comando.Parameters.AddWithValue("IdCategoria", obj.objCategoria.IdCategoria);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    IdProductoGenerado = Convert.ToInt32(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                IdProductoGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdProductoGenerado;
        }

        public bool Actualizar(clsProducto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ACTUALIZAR_PRODUCTO", Conexion);
                    Comando.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    Comando.Parameters.AddWithValue("Codigo", obj.Codigo);
                    Comando.Parameters.AddWithValue("Nombre", obj.Nombre);
                    Comando.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    Comando.Parameters.AddWithValue("IdCategoria", obj.objCategoria.IdCategoria);
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

        public bool Eliminar(clsProducto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ELIMINAR_PRODUCTO", Conexion);
                    Comando.Parameters.AddWithValue("IdProducto", obj.IdProducto);

                    Comando.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(Comando.Parameters["Respuesta"].Value);
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
