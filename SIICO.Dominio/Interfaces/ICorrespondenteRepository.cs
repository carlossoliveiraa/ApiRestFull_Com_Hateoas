using SIICO.Dominio.Entidades;

namespace SIICO.Dominio.Interfaces
{
    public interface ICorrespondenteRepository
    {
        Task<Correspondente?> ObterPorIdAsync(int id);
        Task<Correspondente?> ObterPorCnpjAsync(string cnpj);
        Task<IEnumerable<Correspondente>> ObterTodosAsync();
        Task<Correspondente> AdicionarAsync(Correspondente correspondente);
        Task<Correspondente> AtualizarAsync(Correspondente correspondente);
        Task<bool> ExcluirAsync(int id);
    }
}


