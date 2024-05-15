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
    public class clsD_Categoria
    {
        public List<clsCategoria> Listar()
        {
            List<clsCategoria> Lista = new List<clsCategoria>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdCategoria, Descripcion, Estado FROM CATEGORIA");
                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsCategoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsCategoria>();
                }
                return Lista;
            }
        }
        public int Registrar(clsCategoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdCategoriaGenerado = 0;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_CATEGORIA", Conexion);
                    Comando.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);
                    Comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    IdCategoriaGenerado = Convert.ToInt32(Comando.Parameters["Resultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                IdCategoriaGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdCategoriaGenerado;
        }

        public bool Actualizar(clsCategoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ACTUALIZAR_CATEGORIA", Conexion);
                    Comando.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);
                    Comando.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        public bool Eliminar(clsCategoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ELIMINAR_CATEGORIA", Conexion);
                    Comando.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);

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
