using SIICO.Aplicacao.Dtos;
using SIICO.Aplicacao.Interfaces;
using SIICO.Aplicacao.Request;
using SIICO.Aplicacao.Strategies;
using SIICO.Dominio.Entidades;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Aplicacao.Services
{
    /// <summary>
    /// Service para operações com Correspondentes usando Strategy Pattern
    /// </summary>
    public class CorrespondenteService : ICorrespondenteService
    {
        private readonly CorrespondenteSearchStrategyFactory _strategyFactory;

        public CorrespondenteService(CorrespondenteSearchStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public async Task<HateoasCollectionResponse<CorrespondenteResponseDto>> ObterCorrespondenteAsync(
            CorrespondenteRequest request, 
            string baseUrl)
        {
            // Validar se há filtros informados
            if (!request.TemFiltro())
            {
                throw new ArgumentException(
                    "É necessário informar pelo menos um filtro: Id, Cnpj, Nome, Email ou Telefone.");
            }

            // Obter a estratégia apropriada usando Factory
            var strategy = _strategyFactory.GetStrategy(request);

            // Executar a busca usando a estratégia selecionada
            var (items, totalCount) = await strategy.SearchAsync(
                request, 
                request.Pagina, 
                request.Limite);

            // Criar paginação
            var paginacao = new Paginacao(request.Pagina, request.Limite, totalCount);

            // Gerar links HATEOAS (usando adaptador para CorrespondenteRequest)
            var links = GerarLinksHateoas(request, paginacao, baseUrl, strategy.ProcedureName);

            // Mapear para DTO
            var correspondentes = items.Select(MapToDto);

            return new HateoasCollectionResponse<CorrespondenteResponseDto>(correspondentes, paginacao, links);
        }

        private static CorrespondenteResponseDto MapToDto(Correspondente item) =>
            new()
            {
                Id = item.Id,
                Nome = item.Nome,
                Cnpj = item.Cnpj,
                Email = item.Email,
                Telefone = item.Telefone,
                DataCriacao = item.DataCriacao,
                Ativo = item.Ativo
            };

        private Dictionary<string, Link> GerarLinksHateoas(
            CorrespondenteRequest request, 
            Paginacao paginacao, 
            string baseUrl,
            string procedureName)
        {
            var basePath = $"{baseUrl}/v1/correspondentes";
            var queryParams = BuildQueryString(request);
            
            var links = new Dictionary<string, Link>
            {
                ["self"] = new Link($"{basePath}?{queryParams}&pagina={request.Pagina}", "self", "GET", "Página atual"),
                ["first"] = new Link($"{basePath}?{queryParams}&pagina=1", "first", "GET", "Primeira página")
            };

            if (paginacao.TemPaginaAnterior)
            {
                links["prev"] = new Link($"{basePath}?{queryParams}&pagina={request.Pagina - 1}", "prev", "GET", "Página anterior");
            }

            if (paginacao.TemProximaPagina)
            {
                links["next"] = new Link($"{basePath}?{queryParams}&pagina={request.Pagina + 1}", "next", "GET", "Próxima página");
            }

            if (paginacao.TotalPaginas > 1)
            {
                links["last"] = new Link($"{basePath}?{queryParams}&pagina={paginacao.TotalPaginas}", "last", "GET", "Última página");
            }

            return links;
        }

        private static string BuildQueryString(CorrespondenteRequest request)
        {
            var parts = new List<string>();

            if (request.Id.HasValue)
                parts.Add($"id={request.Id.Value}");

            if (!string.IsNullOrWhiteSpace(request.Cnpj))
                parts.Add($"cnpj={Uri.EscapeDataString(request.Cnpj)}");

            if (!string.IsNullOrWhiteSpace(request.Nome))
                parts.Add($"nome={Uri.EscapeDataString(request.Nome)}");

            if (!string.IsNullOrWhiteSpace(request.Email))
                parts.Add($"email={Uri.EscapeDataString(request.Email)}");

            if (!string.IsNullOrWhiteSpace(request.Telefone))
                parts.Add($"telefone={Uri.EscapeDataString(request.Telefone)}");

            parts.Add($"limite={request.Limite}");

            return string.Join("&", parts);
        }
    }
}

