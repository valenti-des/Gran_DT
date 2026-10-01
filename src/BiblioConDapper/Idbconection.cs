using MySqlConnector;

namespace Biblio.BiblioConDapper
{
    internal class Idbconection
    {
        // Se actualizaron los valores según los datos de la imagen:
        private static readonly string servidor = "127.0.0.1";
        private static readonly string bd = "bd_gran_dt"; // Cambia esto si el nombre de tu base de datos es diferente
        private static readonly string usuario = "5to_agbd";
        private static readonly string password = "Trigg3rs!"; // Reemplaza con la contraseña de este usuario
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
