using SIICO.Dominio.Entidades;
using SIICO.Dominio.Interfaces;

namespace SIICO.Infraestrutura.Repositorio
{
    public class CorrespondenteConvenioRepository : ICorrespondenteConvenioRepository
    {
        private readonly List<CorrespondenteConvenio> _correspondentesConvenios;

        public CorrespondenteConvenioRepository()
        {
            _correspondentesConvenios = GerarDadosMockados();
        }

        public async Task<(IEnumerable<CorrespondenteConvenio> items, int totalCount)> ObterConveniosPorTipoAsync(
            int tipoConvenio, int pagina, int limite)
        {
            await Task.Delay(50);

            var correspondentesFiltrados = _correspondentesConvenios
                .Where(c => c.TipoConvenio == tipoConvenio && c.Ativo)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var items = correspondentesFiltrados
                .Skip((pagina - 1) * limite)
                .Take(limite)
                .ToList();

            return (items, totalCount);
        }

        public async Task<CorrespondenteConvenio> ObterPorIdAsync(int id)
        {
            await Task.Delay(50);
            return _correspondentesConvenios.FirstOrDefault(c => c.Id == id) 
                ?? throw new InvalidOperationException($"CorrespondenteConvenio com ID {id} não encontrado");
        }

        public async Task<CorrespondenteConvenio> AdicionarAsync(CorrespondenteConvenio correspondenteConvenio)
        {
            await Task.Delay(50);
            correspondenteConvenio.Id = _correspondentesConvenios.Max(c => c.Id) + 1;
            _correspondentesConvenios.Add(correspondenteConvenio);
            return correspondenteConvenio;
        }

        public async Task<CorrespondenteConvenio> AtualizarAsync(CorrespondenteConvenio correspondenteConvenio)
        {
            await Task.Delay(50);
            var index = _correspondentesConvenios.FindIndex(c => c.Id == correspondenteConvenio.Id);
            if (index >= 0)
            {
                _correspondentesConvenios[index] = correspondenteConvenio;
            }
            return correspondenteConvenio;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            await Task.Delay(50);
            var correspondente = _correspondentesConvenios.FirstOrDefault(c => c.Id == id);
            if (correspondente != null)
            {
                correspondente.Ativo = false;
                return true;
            }
            return false;
        }

        private static List<CorrespondenteConvenio> GerarDadosMockados()
        {
            var correspondentes = new List<CorrespondenteConvenio>();
            var random = new Random(42);

            for (int i = 1; i <= 25914; i++)
            {
                var tipoConvenio = i <= 10000 ? 1 : random.Next(1, 5);
                
                correspondentes.Add(new CorrespondenteConvenio
                {
                    Id = i,
                    CorrespondenteId = random.Next(1, 1000),
                    NumConvenio = 1000 + i,
                    NumCnpj = GerarCnpj(),
                    NoEmpresa = $"Empresa {i}",
                    NoFantasia = $"Fantasia {i}",
                    TipoConvenio = tipoConvenio,
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
