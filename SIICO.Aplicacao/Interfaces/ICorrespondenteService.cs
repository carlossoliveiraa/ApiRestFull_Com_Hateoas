using SIICO.Aplicacao.Dtos;
using SIICO.Aplicacao.Request;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Aplicacao.Interfaces
{
    public interface ICorrespondenteService
    {
        Task<HateoasCollectionResponse<CorrespondenteResponseDto>> ObterCorrespondenteAsync(
            CorrespondenteRequest request, 
            string baseUrl);
    }
}

