namespace Entidades.MetodosDeExtension
{
    public static class TiempoExtension
    {
        public static double SegundosTranscurriedos(this DateTime inicio)
        { 
            return DateTime.Now.Second - inicio.Second;
        }
    }
}
