using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por Email - executa procedure pr_buscarporEmail
    /// </summary>
    public class EmailSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "pr_buscarporEmail";
        protected override string[] FiltrosRequeridos => new[] { "Email" };
        public override int Prioridade => 70;

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrBuscarPorEmail(
                request.Email!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}

