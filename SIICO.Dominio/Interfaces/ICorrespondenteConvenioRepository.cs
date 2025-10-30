using SIICO.Dominio.Entidades;

namespace SIICO.Dominio.Interfaces
{
    public interface ICorrespondenteConvenioRepository
    {
        Task<(IEnumerable<CorrespondenteConvenio> items, int totalCount)> ObterConveniosPorTipoAsync(
            int tipoConvenio, 
            int pagina, 
            int limite);
        
        Task<CorrespondenteConvenio> ObterPorIdAsync(int id);
        Task<CorrespondenteConvenio> AdicionarAsync(CorrespondenteConvenio correspondenteConvenio);
        Task<CorrespondenteConvenio> AtualizarAsync(CorrespondenteConvenio correspondenteConvenio);
        Task<bool> ExcluirAsync(int id);
    }
}
