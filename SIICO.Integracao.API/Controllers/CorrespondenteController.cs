using Microsoft.AspNetCore.Mvc;
using SIICO.Aplicacao.Interfaces;
using SIICO.Aplicacao.Request;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Integracao.API.Controllers
{
    [ApiController]
    [Route("v1/correspondentes")]
    public class CorrespondenteController : ControllerBase
    {
        private readonly ICorrespondenteService _correspondenteService;

        public CorrespondenteController(ICorrespondenteService correspondenteService)
        {
            _correspondenteService = correspondenteService;
        }

        /// <summary>
        /// Obtém correspondentes com filtros flexíveis usando Strategy Pattern
        /// </summary>
        /// <param name="request">Parâmetros de consulta: Cnpj, Nome, Telefone (combinações suportadas)</param>
        /// <returns>Lista paginada de correspondentes com links HATEOAS</returns>
        /// <remarks>
        /// Filtros suportados (todas as combinações possíveis):
        /// - Id: pr_buscarporId
        /// - Cnpj: pr_buscarcnpj
        /// - Nome: pr_buscarporNome
        /// - Email: pr_buscarporEmail
        /// - Cnpj + Nome: prc_buscarporcnpjNome
        /// - Nome + Telefone: prc_buscarpornomeTeleonfe
        /// - Nome + Email: prc_buscarpornomeEmail
        /// 
        /// O sistema seleciona automaticamente a procedure mais específica baseada nos filtros informados.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(HateoasCollectionResponse<object>), statusCode: 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ObterCorrespondente([FromQuery] CorrespondenteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var response = await _correspondenteService.ObterCorrespondenteAsync(request, baseUrl);
                
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
    }
}

