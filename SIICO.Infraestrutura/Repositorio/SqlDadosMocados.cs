using SIICO.Dominio.Entidades;

namespace SIICO.Infraestrutura.Repositorio
{
    /// <summary>
    /// Classe que contém dados mockados simulando dados do banco de dados
    /// Simula o retorno da procedure pr_BuscarDados
    /// </summary>
    public static class SqlDadosMocados
    {
        private static readonly List<CorrespondenteConvenio> _dadosMockados = GerarDadosMockados();

        /// <summary>
        /// Retorna todos os dados mockados
        /// </summary>
        public static List<CorrespondenteConvenio> ObterTodosDados()
        {
            return _dadosMockados;
        }

        /// <summary>
        /// Simula a execução da procedure pr_BuscarDados
        /// Filtra por tipo de convênio e aplica paginação
        /// </summary>
        public static (IEnumerable<CorrespondenteConvenio> items, int totalCount) SimularProcedurePrBuscarDados(
            int tipoConvenio, int pagina, int limite)
        {
            // Filtrar por tipo de convênio e registros ativos (simulando WHERE TipoConvenio = @TipoConvenio AND Ativo = 1)
            var correspondentesFiltrados = _dadosMockados
                .Where(c => c.TipoConvenio == tipoConvenio && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;

            // Aplicar paginação (simulando OFFSET e FETCH NEXT)
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados
                .Skip(offset)
                .Take(limite)
                .ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Gera dados mockados similares aos que seriam retornados do banco de dados
        /// </summary>
        private static List<CorrespondenteConvenio> GerarDadosMockados()
        {
            var correspondentes = new List<CorrespondenteConvenio>();
            var random = new Random(42); // Seed fixo para dados consistentes

            // Gerar aproximadamente 25914 registros como estava antes
            for (int i = 1; i <= 25914; i++)
            {
                // Distribuir tipos de convênio: primeiros 10000 são tipo 1, depois distribuir entre 1-4
                var tipoConvenio = i <= 10000 ? 1 : random.Next(1, 5);
                
                correspondentes.Add(new CorrespondenteConvenio
                {
                    Id = i,
                    CorrespondenteId = random.Next(1, 1000),
                    NumConvenio = 1000 + i,
                    NumCnpj = GerarCnpj(i),
                    NoEmpresa = $"Empresa {i}",
                    NoFantasia = $"Fantasia {i}",
                    TipoConvenio = tipoConvenio,
                    DataCriacao = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                    Ativo = true
                });
            }

            return correspondentes;
        }

        /// <summary>
        /// Gera um CNPJ fictício baseado no índice
        /// </summary>
        private static string GerarCnpj(int indice)
        {
            // Gerar CNPJ fictício de 14 dígitos baseado no índice
            var cnpjBase = 10000000000000L + indice;
            return cnpjBase.ToString().PadLeft(14, '0');
        }
    }
}

