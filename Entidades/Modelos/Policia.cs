using Entidades.Enumerados;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades.Modelos
{
    public class Policia : IServidorPublico
    {

        private static List<EEmergencia> emergenciasAtendibles;

        static Policia()
        {
            Policia.emergenciasAtendibles = new List<EEmergencia>() { EEmergencia.Accidentes_De_Trafico, EEmergencia.Rescates };
        }
        public string Imagen => $"./assets/{this.GetType().Name}.gif";

        public void Atender(Emergencia emergencia)
        {
            try
            {
                foreach (EEmergencia EM in Policia.emergenciasAtendibles)
                {
                    if (EM == emergencia.Tipo)
                    {
                        emergencia.EstaAtendida = true;
                        break;
                    }
                }
                if (emergencia.EstaAtendida == false)
                {
                    throw new ServidorPublicoInvalidoException("El servidor público no puede atender este tipo de emergencias");
                }
            }
            catch (ServidorPublicoInvalidoException e)
            { }
            catch (Exception)
            { }

        }
    }
}
