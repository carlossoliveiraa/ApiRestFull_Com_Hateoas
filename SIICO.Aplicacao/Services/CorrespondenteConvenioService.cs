using SIICO.Aplicacao.Dtos;
using SIICO.Aplicacao.Interfaces;
using SIICO.Aplicacao.Request;
using SIICO.Dominio.Entidades;
using SIICO.Dominio.Interfaces;
using SIICO.Dominio.ValueObjects;

namespace SIICO.Aplicacao.Services
{
    public class CorrespondenteConvenioService : ICorrespondenteConvenioService
    {
        private readonly ICorrespondenteConvenioRepository _repository;
        private readonly IHateoasService _hateoasService;

        public CorrespondenteConvenioService(ICorrespondenteConvenioRepository repository, IHateoasService hateoasService)
        {
            _repository = repository;
            _hateoasService = hateoasService;
        }

        public async Task<HateoasCollectionResponse<CorrespondenteConvenioDto>> ObterConveniosPorCorrespondenteAsync(
            CorrespondenteConvenioRequest request, string baseUrl)
        {
            var (items, totalCount) = await _repository.ObterConveniosPorTipoAsync(
                request.TipoConvenio, 
                request.Pagina, 
                request.Limite);

            var paginacao = new Paginacao(request.Pagina, request.Limite, totalCount);
            var links = _hateoasService.GerarLinks(request, paginacao, baseUrl);

            var correspondentes = items.Select(MapToDto);

            return new HateoasCollectionResponse<CorrespondenteConvenioDto>(correspondentes, paginacao, links);
        }

        private static CorrespondenteConvenioDto MapToDto(CorrespondenteConvenio item) =>
            new()
            {
                Id = item.Id,
                NumeroConvenio = item.NumConvenio,
                Cnpj = item.NumCnpj,
                NomeEmpresa = item.NoEmpresa,
                NomeFantasia = item.NoFantasia,
                TipoConvenio = item.TipoConvenio,
                DataCriacao = item.DataCriacao,
                Ativo = item.Ativo
            };
    }
}
