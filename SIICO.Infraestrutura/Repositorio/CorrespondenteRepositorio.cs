using SIICO.Dominio.Entidades;
using SIICO.Dominio.Interfaces;

namespace SIICO.Infraestrutura.Repositorio
{
    /// <summary>
    /// Repositório que simula a chamada da procedure pr_BuscarDados
    /// utilizando dados mockados da classe SqlDadosMocados
    /// </summary>
    public class CorrespondenteConvenioRepository : ICorrespondenteConvenioRepository
    {
        public CorrespondenteConvenioRepository()
        {
            // Repositório usando dados mockados - não precisa de configuração de banco
        }

        /// <summary>
        /// Simula a chamada da procedure pr_BuscarDados
        /// Os dados são lidos da classe SqlDadosMocados que contém os dados fictícios
        /// </summary>
        public async Task<(IEnumerable<CorrespondenteConvenio> items, int totalCount)> ObterConveniosPorTipoAsync(
            int tipoConvenio, int pagina, int limite)
        {
            // Simular chamada assíncrona (simulando latência de banco de dados)
            await Task.Delay(50);

            // Simular a execução da procedure pr_BuscarDados usando dados mockados
            var (items, totalCount) = SqlDadosMocados.SimularProcedurePrBuscarDados(
                tipoConvenio, 
                pagina, 
                limite
            );

            return (items, totalCount);
        }
    }
}
