using MySqlConnector;
using System.Data;

namespace Biblio.BiblioConDapper
{
    public class Idbconection
    {
        private static readonly string servidor = Environment.GetEnvironmentVariable("DB_SERVER") ?? "127.0.0.1";
        private static readonly string bd = Environment.GetEnvironmentVariable("DB_NAME") ?? "bd_gran_dt";
        private static readonly string usuario = Environment.GetEnvironmentVariable("DB_USER") ?? "5to_agbd";
        private static readonly string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Trigg3rs!";
        private static readonly string puerto = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";

        private readonly string cadenaConexion = $"server={servidor};database={bd};uid={usuario};pwd={password};port={puerto};";
        private MySqlConnection? conexion;

        public MySqlConnection? EstablecerConexion()
        {
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(cadenaConexion);
                }

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                return conexion;
            }
            catch (MySqlException)
            {
                return null;
            }
        }

        public void CerrarConexion()
        {
            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
