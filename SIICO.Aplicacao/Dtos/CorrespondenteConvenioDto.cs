using System.Text.Json.Serialization;

namespace SIICO.Aplicacao.Dtos
{
    public record CorrespondenteConvenioDto
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("numero_convenio")]
        public int NumeroConvenio { get; init; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; init; } = string.Empty;

        [JsonPropertyName("nome_empresa")]
        public string NomeEmpresa { get; init; } = string.Empty;

        [JsonPropertyName("nome_fantasia")]
        public string NomeFantasia { get; init; } = string.Empty;

        [JsonPropertyName("tipo_convenio")]
        public int TipoConvenio { get; init; }

        [JsonPropertyName("data_criacao")]
        public DateTime DataCriacao { get; init; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; init; }
    }
}


