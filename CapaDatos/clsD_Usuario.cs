using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Collections;

namespace CapaDatos
{
    public class clsD_Usuario
    {
        public List<clsUsuario> Listar() 
        {
            List<clsUsuario> Lista = new List<clsUsuario>();

            using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion FROM USUARIO u");
                    query.AppendLine("INNER JOIN ROL r on r.IdRol = u.IdRol");

                    SqlCommand Comando = new SqlCommand(query.ToString(), Conexion);
                    Comando.CommandType = CommandType.Text;
                    Conexion.Open();

                    using (SqlDataReader dr = Comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new clsUsuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr ["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                objRol = new clsRol() {IdRol = Convert.ToInt32(dr["IdRol"]),Descripcion = dr["Descripcion"].ToString()}



                            });
                        }
                    }

                }
                catch (Exception)
                {

                    Lista = new List<clsUsuario>();
                }
            }
            return Lista;
        }
        public int Registrar(clsUsuario obj, out string Mensaje) 
        { 
            Mensaje = string.Empty;
            int IdUsuarioGenerado = 0;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_REGISTRAR_USUARIO", Conexion);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    Comando.Parameters.AddWithValue("Correo", obj.Correo);
                    Comando.Parameters.AddWithValue("Clave", obj.Clave);
                    Comando.Parameters.AddWithValue("IdRol", obj.objRol.IdRol);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    Comando.CommandType = CommandType.StoredProcedure;
                    Conexion.Open();
                    Comando.ExecuteNonQuery();

                    IdUsuarioGenerado = Convert.ToInt32(Comando.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = Comando.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                IdUsuarioGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdUsuarioGenerado;
        }

        public bool Actualizar(clsUsuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ACTUALIZAR_USUARIO", Conexion);
                    Comando.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    Comando.Parameters.AddWithValue("Documento", obj.Documento);
                    Comando.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    Comando.Parameters.AddWithValue("Correo", obj.Correo);
                    Comando.Parameters.AddWithValue("Clave", obj.Clave);
                    Comando.Parameters.AddWithValue("IdRol", obj.objRol.IdRol);
                    Comando.Parameters.AddWithValue("Estado", obj.Estado);

                    Comando.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        public bool Eliminar(clsUsuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool Respuesta = false;

            try
            {
                using (SqlConnection Conexion = new SqlConnection(clsConexion.Cadena))
                {
                    SqlCommand Comando = new SqlCommand("USP_ELIMINAR_USUARIO", Conexion);
                    Comando.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);

                    Comando.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Comando.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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
