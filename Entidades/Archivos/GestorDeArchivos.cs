using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace Entidades.Archivos
{
    public static class GestorDeArchivos
    {
        private static string basePath;
        static GestorDeArchivos()
        {
            GestorDeArchivos.basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"\\Marguery Lautaro");
            GestorDeArchivos.ValidaExistenciaDeDirectorio();
        }
        private static void Guardar(string informacion, string path) 
        {
            try 
            {
                StreamWriter writer = new StreamWriter(path);
                writer.Write(informacion);
            }
            catch (Exception)
            { }
        }
        public static void Serializar<T>(T elemento, string nombreDelArchivo)
            where T:class
        {
            try 
            {
                string json = JsonSerializer.Serialize(elemento);
                File.WriteAllText(GestorDeArchivos.basePath, json);

            }
            catch(Exception  ex) 
            { }
        }
        private static void ValidaExistenciaDeDirectorio()
        {
            try 
            {
                if (!File.Exists(GestorDeArchivos.basePath))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Marguery Lautaro"));
                }
            }
            catch (Exception)
            { }
        }
    }
}