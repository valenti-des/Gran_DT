using MySqlConnector;
using System.Data;

namespace Biblio.BiblioConDapper
{
    public class Idbconection
    {
        private static readonly string servidor = "Localhost";
        private static readonly string bd = "bd_gran_dt";
        private static readonly string usuario = "root";
        private static readonly string password = "Trigg3rs!"; //Trigg3rs! //12345
        private static readonly string puerto = "3306";

        private readonly string cadenaConexion = Environment.GetEnvironmentVariable("GRANDT_TEST_CONNECTION_STRING")
            ?? $"server={servidor};database={bd};uid={usuario};pwd={password};port={puerto};";
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
