using System;
using SIICO.Dominio.Entidades;

namespace SIICO.Infraestrutura.Repositorio
{
    /// <summary>
    /// Classe que contém dados mockados simulando dados do banco de dados
    /// Simula o retorno das procedures de busca
    /// </summary>
    public static class SqlDadosMocados
    {
        private static readonly List<CorrespondenteConvenio> _dadosMockados = GerarDadosMockados();
        private static readonly List<Correspondente> _correspondentesMockados = GerarCorrespondentesMockados();

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

        #region Procedures de Correspondente

        /// <summary>
        /// Simula a execução da procedure pr_buscarporId
        /// Busca correspondente por ID com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrBuscarPorId(
            int id, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Id == id && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure pr_buscarcnpj
        /// Busca correspondente por CNPJ com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrBuscarCnpj(
            string cnpj, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Cnpj == cnpj && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure pr_buscarporNome
        /// Busca correspondentes por Nome com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrBuscarPorNome(
            string nome, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure pr_buscarporEmail
        /// Busca correspondentes por Email com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrBuscarPorEmail(
            string email, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Email.Contains(email, StringComparison.OrdinalIgnoreCase) && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure prc_buscarpornomeTeleonfe
        /// Busca correspondentes por Nome e Telefone com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrcBuscarPorNomeTelefone(
            string nome, string telefone, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) 
                    && c.Telefone == telefone 
                    && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure prc_buscarpornomeEmail
        /// Busca correspondentes por Nome e Email com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrcBuscarPorNomeEmail(
            string nome, string email, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) 
                    && c.Email.Contains(email, StringComparison.OrdinalIgnoreCase) 
                    && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Simula a execução da procedure prc_buscarporcnpjNome
        /// Busca correspondentes por CNPJ e Nome com paginação
        /// </summary>
        public static (IEnumerable<Correspondente> items, int totalCount) SimularProcedurePrcBuscarPorCnpjNome(
            string cnpj, string nome, int pagina, int limite)
        {
            var correspondentesFiltrados = _correspondentesMockados
                .Where(c => c.Cnpj == cnpj 
                    && c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) 
                    && c.Ativo)
                .OrderBy(c => c.Id)
                .ToList();

            var totalCount = correspondentesFiltrados.Count;
            var offset = (pagina - 1) * limite;
            var items = correspondentesFiltrados.Skip(offset).Take(limite).ToList();

            return (items, totalCount);
        }

        /// <summary>
        /// Gera dados mockados de Correspondentes
        /// </summary>
        private static List<Correspondente> GerarCorrespondentesMockados()
        {
            var correspondentes = new List<Correspondente>();
            var random = new Random(42); // Seed fixo para dados consistentes

            // Gerar 1000 correspondentes
            for (int i = 1; i <= 1000; i++)
            {
                correspondentes.Add(new Correspondente
                {
                    Id = i,
                    Nome = $"Correspondente {i}",
                    Cnpj = GerarCnpjCorrespondente(i),
                    Email = $"correspondente{i}@email.com",
                    Telefone = GerarTelefone(i, random),
                    DataCriacao = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                    Ativo = true
                });
            }

            return correspondentes;
        }

        /// <summary>
        /// Gera CNPJ para correspondente
        /// </summary>
        private static string GerarCnpjCorrespondente(int indice)
        {
            var cnpjBase = 20000000000000L + indice;
            return cnpjBase.ToString().PadLeft(14, '0');
        }

        /// <summary>
        /// Gera telefone fictício
        /// </summary>
        private static string GerarTelefone(int indice, Random random)
        {
            // Gerar telefone no formato (XX) XXXXX-XXXX
            var ddd = random.Next(11, 99);
            var numero = (100000000 + indice).ToString().PadLeft(9, '0');
            return $"{ddd}{numero}";
        }

        #endregion
    }
}


