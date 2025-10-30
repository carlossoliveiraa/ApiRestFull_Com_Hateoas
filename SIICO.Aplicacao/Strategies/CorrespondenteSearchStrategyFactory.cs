using SIICO.Aplicacao.Request;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Factory para criar a estratégia de busca apropriada baseada no request
    /// Utiliza sistema de prioridade para selecionar a estratégia mais específica
    /// </summary>
    public class CorrespondenteSearchStrategyFactory
    {
        private readonly IEnumerable<ICorrespondenteSearchStrategy> _strategies;

        public CorrespondenteSearchStrategyFactory(IEnumerable<ICorrespondenteSearchStrategy> strategies)
        {
            _strategies = strategies.OrderByDescending(s => (s as BaseSearchStrategy)?.Prioridade ?? 0).ToList();
        }

        /// <summary>
        /// Retorna a estratégia apropriada para o request fornecido
        /// Prioriza estratégias mais específicas (maior número de filtros e maior prioridade)
        /// </summary>
        public ICorrespondenteSearchStrategy GetStrategy(CorrespondenteRequest request)
        {
            // Busca estratégias que podem lidar com o request, ordenadas por prioridade
            var estrategiasValidas = _strategies
                .Where(s => s.CanHandle(request))
                .OrderByDescending(s => (s as BaseSearchStrategy)?.Prioridade ?? 0)
                .ToList();

            if (!estrategiasValidas.Any())
            {
                var filtrosInformados = request.ObterChaveCombinacao();
                throw new InvalidOperationException(
                    $"Nenhuma estratégia de busca encontrada para a combinação de filtros: {filtrosInformados}. " +
                    "Informe pelo menos um dos seguintes filtros: Id, Cnpj, Nome, Email ou Telefone.");
            }

            // Retorna a estratégia com maior prioridade
            return estrategiasValidas.First();
        }
    }
}

