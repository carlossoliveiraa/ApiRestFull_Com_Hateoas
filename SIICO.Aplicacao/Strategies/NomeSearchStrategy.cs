using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por Nome - executa procedure pr_buscarporNome
    /// </summary>
    public class NomeSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "pr_buscarporNome";
        protected override string[] FiltrosRequeridos => new[] { "Nome" };
        public override int Prioridade => 80;

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrBuscarPorNome(
                request.Nome!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}
