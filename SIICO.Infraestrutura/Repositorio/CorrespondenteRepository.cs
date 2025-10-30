using SIICO.Dominio.Entidades;
using SIICO.Dominio.Interfaces;

namespace SIICO.Infraestrutura.Repositorio
{
    public class CorrespondenteRepository : ICorrespondenteRepository
    {
        private readonly List<Correspondente> _correspondentes;

        public CorrespondenteRepository()
        {
            _correspondentes = GerarDadosMockados();
        }

        public async Task<Correspondente?> ObterPorIdAsync(int id)
        {
            await Task.Delay(50);
            return _correspondentes.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Correspondente?> ObterPorCnpjAsync(string cnpj)
        {
            await Task.Delay(50);
            return _correspondentes.FirstOrDefault(c => c.Cnpj == cnpj);
        }

        public async Task<IEnumerable<Correspondente>> ObterTodosAsync()
        {
            await Task.Delay(100);
            return _correspondentes.Where(c => c.Ativo).ToList();
        }

        public async Task<Correspondente> AdicionarAsync(Correspondente correspondente)
        {
            await Task.Delay(50);
            correspondente.Id = _correspondentes.Max(c => c.Id) + 1;
            _correspondentes.Add(correspondente);
            return correspondente;
        }

        public async Task<Correspondente> AtualizarAsync(Correspondente correspondente)
        {
            await Task.Delay(50);
            var index = _correspondentes.FindIndex(c => c.Id == correspondente.Id);
            if (index >= 0)
            {
                _correspondentes[index] = correspondente;
            }
            return correspondente;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            await Task.Delay(50);
            var correspondente = _correspondentes.FirstOrDefault(c => c.Id == id);
            if (correspondente != null)
            {
                correspondente.Ativo = false;
                return true;
            }
            return false;
        }

        private static List<Correspondente> GerarDadosMockados()
        {
            var correspondentes = new List<Correspondente>();
            var random = new Random(42);

            for (int i = 1; i <= 1000; i++)
            {
                correspondentes.Add(new Correspondente
                {
                    Id = i,
                    Nome = $"Correspondente {i}",
                    Cnpj = GerarCnpj(),
                    Email = $"correspondente{i}@email.com",
                    Telefone = $"119{random.Next(10000000, 99999999)}",
                    DataCriacao = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                    Ativo = true
                });
            }

            return correspondentes;
        }

        private static string GerarCnpj()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(0, 14).Select(_ => random.Next(0, 9)));
        }
    }
}
