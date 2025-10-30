using SIICO.Aplicacao.Dtos;
using SIICO.Aplicacao.Request;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Aplicacao.Interfaces
{
    public interface ICorrespondenteConvenioService
    {
        Task<HateoasCollectionResponse<CorrespondenteConvenioDto>> ObterConveniosPorCorrespondenteAsync(
            CorrespondenteConvenioRequest request, string baseUrl);
    }
}
