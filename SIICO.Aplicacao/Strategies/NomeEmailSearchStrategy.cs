using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por Nome e Email - executa procedure prc_buscarpornomeEmail
    /// </summary>
    public class NomeEmailSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "prc_buscarpornomeEmail";
        protected override string[] FiltrosRequeridos => new[] { "Nome", "Email" };
        public override int Prioridade => 50;

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrcBuscarPorNomeEmail(
                request.Nome!,
                request.Email!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}

