using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por ID - executa procedure pr_buscarporId
    /// </summary>
    public class IdSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "pr_buscarporId";
        protected override string[] FiltrosRequeridos => new[] { "Id" };
        public override int Prioridade => 100; // Alta prioridade para busca por ID (único)

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrBuscarPorId(
                request.Id!.Value,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}

