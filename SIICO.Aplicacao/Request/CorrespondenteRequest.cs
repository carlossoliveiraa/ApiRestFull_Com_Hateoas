using System.ComponentModel.DataAnnotations;

namespace SIICO.Aplicacao.Request
{
    /// <summary>
    /// Request para busca de correspondentes com filtros flexíveis
    /// Suporta diferentes combinações de filtros que acionam procedures específicas
    /// </summary>
    public record CorrespondenteRequest
    {
        /// <summary>
        /// ID do correspondente (filtro opcional)
        /// </summary>
        public int? Id { get; init; }

        /// <summary>
        /// Nome do correspondente (filtro opcional)
        /// </summary>
        public string? Nome { get; init; }

        /// <summary>
        /// CNPJ do correspondente (filtro opcional)
        /// </summary>
        public string? Cnpj { get; init; }

        /// <summary>
        /// Email do correspondente (filtro opcional)
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Telefone do correspondente (filtro opcional)
        /// </summary>
        public string? Telefone { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "Página deve ser maior que 0")]
        public int Pagina { get; init; } = 1;

        [Range(1, 100, ErrorMessage = "Limite deve estar entre 1 e 100")]
        public int Limite { get; init; } = 10;

        /// <summary>
        /// Valida se pelo menos um filtro foi informado
        /// </summary>
        public bool TemFiltro()
        {
            return Id.HasValue
                || !string.IsNullOrWhiteSpace(Cnpj)
                || !string.IsNullOrWhiteSpace(Nome)
                || !string.IsNullOrWhiteSpace(Email)
                || !string.IsNullOrWhiteSpace(Telefone);
        }

        /// <summary>
        /// Retorna a chave de combinação de filtros para identificar a procedure correta
        /// </summary>
        public string ObterChaveCombinacao()
        {
            var filtrosAtivos = new List<string>();

            if (Id.HasValue) filtrosAtivos.Add("Id");
            if (!string.IsNullOrWhiteSpace(Cnpj)) filtrosAtivos.Add("Cnpj");
            if (!string.IsNullOrWhiteSpace(Nome)) filtrosAtivos.Add("Nome");
            if (!string.IsNullOrWhiteSpace(Email)) filtrosAtivos.Add("Email");
            if (!string.IsNullOrWhiteSpace(Telefone)) filtrosAtivos.Add("Telefone");

            return string.Join("_", filtrosAtivos);
        }
    }
}
