using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por CNPJ - executa procedure pr_buscarcnpj
    /// </summary>
    public class CnpjSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "pr_buscarcnpj";
        protected override string[] FiltrosRequeridos => new[] { "Cnpj" };
        public override int Prioridade => 90;

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrBuscarCnpj(
                request.Cnpj!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}
