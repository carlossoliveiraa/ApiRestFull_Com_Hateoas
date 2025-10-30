-- Procedure: pr_BuscarDados
-- Descrição: Busca dados de correspondentes convênios com paginação baseada nos parâmetros do HateoasService
-- Parâmetros:
--   @TipoConvenio: Tipo do convênio a ser filtrado
--   @Pagina: Número da página (baseado no HateoasService)
--   @Limite: Quantidade de registros por página (baseado no HateoasService)
--   @TotalRegistros: Parâmetro de saída com o total de registros encontrados

CREATE OR ALTER PROCEDURE pr_BuscarDados
    @TipoConvenio INT,
    @Pagina INT,
    @Limite INT,
    @TotalRegistros INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calcular o offset baseado na página e limite
    DECLARE @Offset INT = (@Pagina - 1) * @Limite;

    -- Contar o total de registros que atendem ao filtro
    SELECT @TotalRegistros = COUNT(*)
    FROM CorrespondenteConvenio
    WHERE TipoConvenio = @TipoConvenio
      AND Ativo = 1;

    -- Buscar os registros paginados
    SELECT 
        Id,
        CorrespondenteId,
        NumConvenio,
        NumCnpj,
        NoEmpresa,
        NoFantasia,
        TipoConvenio,
        DataCriacao,
        Ativo
    FROM CorrespondenteConvenio
    WHERE TipoConvenio = @TipoConvenio
      AND Ativo = 1
    ORDER BY Id
    OFFSET @Offset ROWS
    FETCH NEXT @Limite ROWS ONLY;
END;

