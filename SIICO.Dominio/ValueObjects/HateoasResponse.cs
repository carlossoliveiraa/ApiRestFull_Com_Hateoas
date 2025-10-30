using System.Text.Json.Serialization;

namespace SIICO.Dominio.ValueObjects
{
    public class HateoasCollectionResponse<T>
    {
        [JsonPropertyName("links")]
        public LinksResponse Links { get; set; } = new();

        [JsonPropertyName("paginacao")]
        public PaginacaoResponse Paginacao { get; set; } = new();

        [JsonPropertyName("correspondentes")]
        public IEnumerable<T> Correspondentes { get; set; } = new List<T>();

        public HateoasCollectionResponse(IEnumerable<T> items, Paginacao paginacao, Dictionary<string, Link> links)
        {
            Correspondentes = items;
            Paginacao = new PaginacaoResponse
            {
                Pagina = paginacao.Pagina,
                RegistrosPagina = paginacao.Limite,
                TotalPaginas = paginacao.TotalPaginas,
                TotalRegistrosEncontrados = paginacao.TotalRegistros
            };
            Links = ConvertLinks(links);
        }

        private static LinksResponse ConvertLinks(Dictionary<string, Link> links)
        {
            if (links == null)
            {
                return new LinksResponse();
            }

            return new LinksResponse
            {
                PaginaAtual = links.ContainsKey("self") && links["self"] != null ? links["self"].Href : string.Empty,
                PaginaAnterior = links.ContainsKey("prev") && links["prev"] != null ? links["prev"].Href : null,
                ProximaPagina = links.ContainsKey("next") && links["next"] != null ? links["next"].Href : null,
                PrimeiraPagina = links.ContainsKey("first") && links["first"] != null ? links["first"].Href : string.Empty,
                UltimaPagina = links.ContainsKey("last") && links["last"] != null ? links["last"].Href : string.Empty
            };
        }
    }

    public class LinksResponse
    {
        [JsonPropertyName("pagina_atual")]
        public string PaginaAtual { get; set; } = string.Empty;

        [JsonPropertyName("pagina_anterior")]
        public string? PaginaAnterior { get; set; }

        [JsonPropertyName("proxima_pagina")]
        public string? ProximaPagina { get; set; }

        [JsonPropertyName("primeira_pagina")]
        public string PrimeiraPagina { get; set; } = string.Empty;

        [JsonPropertyName("ultima_pagina")]
        public string UltimaPagina { get; set; } = string.Empty;
    }

    public class PaginacaoResponse
    {
        [JsonPropertyName("pagina")]
        public int Pagina { get; set; }

        [JsonPropertyName("registros_pagina")]
        public int RegistrosPagina { get; set; }

        [JsonPropertyName("total_paginas")]
        public int TotalPaginas { get; set; }

        [JsonPropertyName("total_registros_encontrados")]
        public int TotalRegistrosEncontrados { get; set; }
    }
}
