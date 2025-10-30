using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Interface Strategy para diferentes estratégias de busca de correspondentes
    /// </summary>
    public interface ICorrespondenteSearchStrategy
    {
        /// <summary>
        /// Nome da procedure que será executada
        /// </summary>
        string ProcedureName { get; }

        /// <summary>
        /// Verifica se esta estratégia pode ser usada para o request fornecido
        /// </summary>
        bool CanHandle(CorrespondenteRequest request);

        /// <summary>
        /// Executa a busca usando a estratégia específica
        /// </summary>
        Task<(IEnumerable<Correspondente> items, int totalCount)> SearchAsync(
            CorrespondenteRequest request, 
            int pagina, 
            int limite);
    }
}

