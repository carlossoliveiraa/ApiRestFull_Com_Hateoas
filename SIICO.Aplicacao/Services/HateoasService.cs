using SIICO.Aplicacao.Request;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Aplicacao.Services
{
    public interface IHateoasService
    {
        Dictionary<string, Link> GerarLinks(CorrespondenteConvenioRequest request, Paginacao paginacao, string baseUrl);
    }

    public class HateoasService : IHateoasService
    {
        public Dictionary<string, Link> GerarLinks(CorrespondenteConvenioRequest request, Paginacao paginacao, string baseUrl)
        {
            var basePath = $"{baseUrl}/v1/correspondentes/convenio";
            var queryParams = $"tipo-convenio={request.TipoConvenio}&limite={request.Limite}";
            
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
    }
}
