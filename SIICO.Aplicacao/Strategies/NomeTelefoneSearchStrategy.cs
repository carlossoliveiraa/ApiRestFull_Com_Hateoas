using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por Nome e Telefone - executa procedure prc_buscarpornomeTeleonfe
    /// </summary>
    public class NomeTelefoneSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "prc_buscarpornomeTeleonfe";
        protected override string[] FiltrosRequeridos => new[] { "Nome", "Telefone" };
        public override int Prioridade => 60; // Combinação específica

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrcBuscarPorNomeTelefone(
                request.Nome!,
                request.Telefone!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}
