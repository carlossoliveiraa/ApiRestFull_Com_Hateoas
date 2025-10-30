namespace SIICO.Aplicacao.Response
{
    public class CorrespondenteConvenioResponse
    {
        public int Id { get; set; }
        public int NumConvenio { get; set; }
        public string NumCnpj { get; set; } = string.Empty;
        public string NoEmpresa { get; set; } = string.Empty;
        public string NoFantasia { get; set; } = string.Empty;
        public int TipoConvenio { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }

    public class CorrespondenteConvenioListResponse
    {
        public LinksResponse Links { get; set; } = new();
        public PaginacaoResponse Paginacao { get; set; } = new();
        public IEnumerable<CorrespondenteConvenioResponse> Correspondentes { get; set; } = new List<CorrespondenteConvenioResponse>();
    }

    public class LinksResponse
    {
        public string PaginaAtual { get; set; } = string.Empty;
        public string? PaginaAnterior { get; set; }
        public string? ProximaPagina { get; set; }
        public string PrimeiraPagina { get; set; } = string.Empty;
        public string UltimaPagina { get; set; } = string.Empty;
    }

    public class PaginacaoResponse
    {
        public int Pagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistrosEncontrados { get; set; }
    }
}
