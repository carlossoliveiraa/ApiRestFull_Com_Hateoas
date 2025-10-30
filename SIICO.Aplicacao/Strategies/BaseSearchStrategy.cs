using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;

namespace SIICO.Aplicacao.Strategies
{
    /// <summary>
    /// Classe base abstrata para estratégias de busca
    /// Fornece funcionalidades comuns e estrutura para implementações específicas
    /// </summary>
    public abstract class BaseSearchStrategy : ICorrespondenteSearchStrategy
    {
        public abstract string ProcedureName { get; }
        
        /// <summary>
        /// Define quais filtros são necessários para esta estratégia
        /// </summary>
        protected abstract string[] FiltrosRequeridos { get; }

        /// <summary>
        /// Define a ordem de prioridade (maior número = maior prioridade)
        /// Estratégias mais específicas devem ter prioridade maior
        /// </summary>
        public abstract int Prioridade { get; }

        public bool CanHandle(CorrespondenteRequest request)
        {
            return ValidarFiltros(request);
        }

        public async Task<(IEnumerable<Correspondente> items, int totalCount)> SearchAsync(
            CorrespondenteRequest request, 
            int pagina, 
            int limite)
        {
            await Task.Delay(50); // Simular latência de banco
            
            return await ExecutarBusca(request, pagina, limite);
        }

        /// <summary>
        /// Valida se o request contém todos os filtros necessários para esta estratégia
        /// </summary>
        protected virtual bool ValidarFiltros(CorrespondenteRequest request)
        {
            foreach (var filtro in FiltrosRequeridos)
            {
                if (!EstaFiltroAtivo(request, filtro))
                {
                    return false;
                }
            }

            // Verifica se não há filtros extras que não são suportados por esta estratégia
            return NaoTemFiltrosExtras(request);
        }

        /// <summary>
        /// Verifica se um filtro específico está ativo no request
        /// </summary>
        protected bool EstaFiltroAtivo(CorrespondenteRequest request, string filtro)
        {
            return filtro switch
            {
                "Id" => request.Id.HasValue,
                "Cnpj" => !string.IsNullOrWhiteSpace(request.Cnpj),
                "Nome" => !string.IsNullOrWhiteSpace(request.Nome),
                "Email" => !string.IsNullOrWhiteSpace(request.Email),
                "Telefone" => !string.IsNullOrWhiteSpace(request.Telefone),
                _ => false
            };
        }

        /// <summary>
        /// Verifica se não há filtros extras além dos suportados por esta estratégia
        /// </summary>
        protected virtual bool NaoTemFiltrosExtras(CorrespondenteRequest request)
        {
            var filtrosAtivos = ObterFiltrosAtivos(request);
            return filtrosAtivos.All(f => FiltrosRequeridos.Contains(f));
        }

        /// <summary>
        /// Obtém lista de filtros ativos no request
        /// </summary>
        protected List<string> ObterFiltrosAtivos(CorrespondenteRequest request)
        {
            var filtros = new List<string>();
            
            if (request.Id.HasValue) filtros.Add("Id");
            if (!string.IsNullOrWhiteSpace(request.Cnpj)) filtros.Add("Cnpj");
            if (!string.IsNullOrWhiteSpace(request.Nome)) filtros.Add("Nome");
            if (!string.IsNullOrWhiteSpace(request.Email)) filtros.Add("Email");
            if (!string.IsNullOrWhiteSpace(request.Telefone)) filtros.Add("Telefone");
            
            return filtros;
        }

        /// <summary>
        /// Executa a busca específica da estratégia
        /// </summary>
        protected abstract Task<(IEnumerable<Correspondente> items, int totalCount)> ExecutarBusca(
            CorrespondenteRequest request, 
            int pagina, 
            int limite);
    }
}

