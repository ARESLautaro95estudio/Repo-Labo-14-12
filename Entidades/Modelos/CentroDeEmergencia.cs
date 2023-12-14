using Entidades.Delegados;
using Entidades.Enumerados;
using Entidades.Interfaces;

namespace Entidades.Modelos
{


    public class CentroDeEmergencia
    {
        private CancellationTokenSource cancellation;
        private string nombre;
        private Emergencia emergenciaEnCurso;
        private List<Emergencia> emergenciasAtendidas;
        public event DelegadoEmergenciaEnCurso OnEmergenciaEnCurso;
        public event DelegadoEstadoEmergiaEnCurso OnEstadoEmergiaEnCurso;
        public event DelegadoEmergenciaMensaje OnServidorInvalido;
        public CentroDeEmergencia(string nombre)
        {
            this.nombre = nombre;
            this.emergenciasAtendidas = new List<Emergencia>();
        }

        public string Nombre { get => this.nombre; }
        public List<Emergencia> EmergenciasAtendidas { get => this.emergenciasAtendidas; }

        public void HabilitarIngreso()
        {
            Task.Run(() =>
            {
                Random rnd = new Random();
                this.emergenciaEnCurso = new Emergencia((EEmergencia)rnd.Next(0, 4));
                while (!this.cancellation.IsCancellationRequested)
                {
                    this.OnEmergenciaEnCurso(this.emergenciaEnCurso);
                    this.DarSeguimientoAEmergencia();
                }
            });
        }
        private void DarSeguimientoAEmergencia()
        {
           
            if (this.OnEmergenciaEnCurso is not null)
            {
                while (!this.cancellation.IsCancellationRequested
                    && this.emergenciaEnCurso.SegundosTranscurridos < Emergencia.TiempoLimiteEnSegundos&&
                    this.emergenciaEnCurso.EstaAtendida==false)
                {
                    Thread.Sleep(1000);
                    this.emergenciaEnCurso.ActualizarEstadoEmergencia();
                    this.NotificarEstadoDeEmergenciaEnCurso();

                }
            }
            else { this.OnEmergenciaEnCurso.Invoke(this.emergenciaEnCurso);}
       
        }
        public void EnviarServidorPublico<T>(T servidorPublico) where T : IServidorPublico
        {
            Task.Run(() => {
                Thread.Sleep(3000);
                servidorPublico.Atender(this.emergenciaEnCurso);
                if (this.emergenciaEnCurso.EstaAtendida)
                {
                    this.emergenciasAtendidas.Add(this.emergenciaEnCurso);
                }
                else 
                {
                    this.OnServidorInvalido.Invoke("Servidor publico invalido");
                }

            });
        }
        public void DeshabilitarIngreso()
        {
            this.cancellation.Cancel();
        }
        private void NotificarEstadoDeEmergenciaEnCurso()
        {
            this.OnEmergenciaEnCurso.Invoke(this.emergenciaEnCurso); ;
        }
    }
}
