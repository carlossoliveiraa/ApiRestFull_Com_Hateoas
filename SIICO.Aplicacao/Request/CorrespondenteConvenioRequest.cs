using System.ComponentModel.DataAnnotations;

namespace SIICO.Aplicacao.Request
{
    public record CorrespondenteConvenioRequest
    {
        [Required(ErrorMessage = "Tipo de convênio é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Tipo de convênio deve ser maior que 0")]
        public int TipoConvenio { get; init; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Página deve ser maior que 0")]
        public int Pagina { get; init; } = 1;
        
        [Range(1, 100, ErrorMessage = "Limite deve estar entre 1 e 100")]
        public int Limite { get; init; } = 10;

        public string ToQueryString() => $"tipo-convenio={TipoConvenio}&limite={Limite}&pagina={Pagina}";
    }
}
