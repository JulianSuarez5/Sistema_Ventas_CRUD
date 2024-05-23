using System.Data.SqlClient;

namespace CapaDatos
{
    public class clsConexion
    {
        private static string servidor = "sqlserver1.czo4ooikuy80.us-east-2.rds.amazonaws.com";
        private static string baseDatos = "BD_SISTEMA_VENTAS";
        private static string usuario = "Julian";
        private static string password = "Juaco666";

        public static string Cadena
        {
            get
            {
                // Construye la cadena de conexión
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = servidor;
                builder.InitialCatalog = baseDatos;
                builder.UserID = usuario;
                builder.Password = password;
                builder.Encrypt = true;
                builder.TrustServerCertificate = true;

                return builder.ConnectionString;
            }
        }
    }
}