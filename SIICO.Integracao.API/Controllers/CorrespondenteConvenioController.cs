using Microsoft.AspNetCore.Mvc;
using SIICO.Aplicacao.Interfaces;
using SIICO.Aplicacao.Request;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Integracao.API.Controllers
{
    [ApiController]
    [Route("v1/correspondentes")]
    public class CorrespondenteConvenioController : ControllerBase
    {
        private readonly ICorrespondenteConvenioService _convenioService;

        public CorrespondenteConvenioController(ICorrespondenteConvenioService convenioService)
        {
            _convenioService = convenioService;
        }

        /// <summary>
        /// Obtém convênios por correspondente com paginação e HATEOAS
        /// </summary>
        /// <param name="request">Parâmetros de consulta incluindo tipo de convênio, página e limite</param>
        /// <returns>Lista paginada de correspondentes com links HATEOAS</returns>
        [HttpGet("convenio")]
        [ProducesResponseType(typeof(HateoasCollectionResponse<object>), statusCode: 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ObterConveniosPorCorrespondente([FromQuery] CorrespondenteConvenioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var response = await _convenioService.ObterConveniosPorCorrespondenteAsync(request, baseUrl);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
    }
}
