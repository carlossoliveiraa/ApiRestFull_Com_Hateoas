using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SIICO.Aplicacao.Dtos
{

    public class CorrespondenteDto
    {
        [JsonProperty("NumeroConvenio")]
        public int NumeroConvenio { get; set; }

        [JsonProperty("NumeroCnpj")]
        public string NumeroCnpj { get; set; } = string.Empty;

        [JsonProperty("NomeEmpresa")]
        public string NomeEmpresa { get; set; } = string.Empty;

        [JsonProperty("NomeFantasia")]
        public string NomeFantasia { get; set; } = string.Empty;
    }
}
