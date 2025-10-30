using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Infraestrutura.Repositorio;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Estratégia de busca por CNPJ e Nome - executa procedure prc_buscarporcnpjNome
    /// </summary>
    public class CnpjNomeSearchStrategy : BaseSearchStrategy
    {
        public override string ProcedureName => "prc_buscarporcnpjNome";
        protected override string[] FiltrosRequeridos => new[] { "Cnpj", "Nome" };
        public override int Prioridade => 55;

        protected override Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrcBuscarPorCnpjNome(
                request.Cnpj!,
                request.Nome!,
                pagina,
                limite
            );

            return Task.FromResult((items, totalCount));
        }
    }
}

