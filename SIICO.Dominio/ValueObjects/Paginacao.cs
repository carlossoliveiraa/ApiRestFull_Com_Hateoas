namespace SIICO.Dominio.ValueObjects
{
    public class Paginacao
    {
        public int Pagina { get; set; }
        public int Limite { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / Limite);
        public bool TemPaginaAnterior => Pagina > 1;
        public bool TemProximaPagina => Pagina < TotalPaginas;

        public Paginacao(int pagina, int limite, int totalRegistros)
        {
            Pagina = pagina;
            Limite = limite;
            TotalRegistros = totalRegistros;
        }

        public int CalcularOffset() => (Pagina - 1) * Limite;
    }
}
