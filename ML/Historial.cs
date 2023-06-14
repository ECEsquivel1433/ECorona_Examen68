namespace ML
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public int Numero { get; set; }

        public int Resultado { get; set; }

        public string FechaHora { get; set; } = default!;
        public List<object> Historiales { get; set; } = default!;

        public ML.Usuario Usuario { get; set; } = default!;
        public int IdUsuario { get; set; }
    }
}