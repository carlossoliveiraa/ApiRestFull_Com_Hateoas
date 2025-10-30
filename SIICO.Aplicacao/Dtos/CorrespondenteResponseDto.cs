using System.Text.Json.Serialization;

namespace SIICO.Aplicacao.Dtos
{
    /// <summary>
    /// DTO de resposta para Correspondente
    /// </summary>
    public record CorrespondenteResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("nome")]
        public string Nome { get; init; } = string.Empty;

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; init; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; init; } = string.Empty;

        [JsonPropertyName("telefone")]
        public string Telefone { get; init; } = string.Empty;

        [JsonPropertyName("data_criacao")]
        public DateTime DataCriacao { get; init; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; init; }
    }
}

