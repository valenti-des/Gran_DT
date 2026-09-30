using MySqlConnector;

namespace BiblioConDapper
{
    internal class Conexion
    {
        private static readonly string servidor = "localhost";
        private static readonly string bd = "Gran_DT";
        private static readonly string usuario = "Victor";
        private static readonly string password = "";
        private static readonly string puerto = "3306";

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

                if (conexion.State != System.Data.ConnectionState.Open)
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
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

    }
}