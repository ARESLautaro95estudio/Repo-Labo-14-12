using System.Data.SqlClient;

namespace Entidades.BaseDeDatos
{
    public static class GestorDeBaseDeDatos
    {
        private static SqlConnection connection;
        private static string stringConnection;
        static GestorDeBaseDeDatos()
        { 
            
            GestorDeBaseDeDatos.stringConnection = "Server=.\\MSSQLSERVER01;Database=MargueryLautaro1412 ;Trusted_Connection=True;";
        }
        public static bool RegistrarTrabajo(string nombreAlumno, int cantidadPacientes)
        {
            try 
            {
                SqlConnection connection = new SqlConnection(GestorDeBaseDeDatos.stringConnection);
                string QWERY = $"INSERT INTO dbo.log (pacientes_atendidos,alumno)\r\n  VALUES ({cantidadPacientes}, {nombreAlumno}); ";
                SqlCommand comando = new SqlCommand(QWERY, GestorDeBaseDeDatos.connection);
                comando.ExecuteScalar();
            }
            catch 
            {
                return false;            
            }
            return true;
        }
    }
}