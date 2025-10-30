using SIICO.Dominio.Entidades;

namespace SIICO.Dominio.Interfaces
{
    public interface ICorrespondenteConvenioRepository
    {
        Task<(IEnumerable<CorrespondenteConvenio> items, int totalCount)> ObterConveniosPorTipoAsync(
            int tipoConvenio, 
            int pagina, 
            int limite);
    }
}
