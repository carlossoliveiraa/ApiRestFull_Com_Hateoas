-- Script para inserir dados de exemplo na tabela CorrespondenteConvenio
-- Este script simula dados do banco para testes

-- Limpar dados existentes (opcional - descomente se necessário)
-- TRUNCATE TABLE [dbo].[CorrespondenteConvenio];

-- Inserir dados de exemplo
SET NOCOUNT ON;

DECLARE @Contador INT = 1;
DECLARE @TotalRegistros INT = 1000; -- Altere este valor para inserir mais registros
DECLARE @TipoConvenio INT;
DECLARE @RandomCorrespondenteId INT;
DECLARE @NumCnpj VARCHAR(14);

WHILE @Contador <= @TotalRegistros
BEGIN
    -- Gerar tipo de convênio (1-4)
    SET @TipoConvenio = CASE 
        WHEN @Contador <= 250 THEN 1
        WHEN @Contador <= 500 THEN 2
        WHEN @Contador <= 750 THEN 3
        ELSE 4
    END;
    
    -- Gerar ID de correspondente aleatório (1-100)
    SET @RandomCorrespondenteId = (@Contador % 100) + 1;
    
    -- Gerar CNPJ fictício (apenas para exemplo)
    SET @NumCnpj = RIGHT('00000000000000' + CAST((10000000000000 + @Contador) AS VARCHAR(14)), 14);
    
    INSERT INTO [dbo].[CorrespondenteConvenio] (
        [CorrespondenteId],
        [NumConvenio],
        [NumCnpj],
        [NoEmpresa],
        [NoFantasia],
        [TipoConvenio],
        [DataCriacao],
        [Ativo]
    )
    VALUES (
        @RandomCorrespondenteId,
        1000 + @Contador,
        @NumCnpj,
        N'Empresa ' + CAST(@Contador AS NVARCHAR(10)),
        N'Fantasia ' + CAST(@Contador AS NVARCHAR(10)),
        @TipoConvenio,
        DATEADD(DAY, -ABS(CHECKSUM(NEWID()) % 365), GETUTCDATE()),
        1
    );
    
    SET @Contador = @Contador + 1;
    
    -- Mostrar progresso a cada 100 registros
    IF @Contador % 100 = 0
    BEGIN
        PRINT 'Inseridos ' + CAST(@Contador AS VARCHAR(10)) + ' registros...';
    END;
END;

SET NOCOUNT OFF;

PRINT 'Total de ' + CAST(@TotalRegistros AS VARCHAR(10)) + ' registros inseridos com sucesso!';

-- Verificar distribuição por tipo de convênio
SELECT 
    TipoConvenio,
    COUNT(*) AS TotalRegistros,
    SUM(CASE WHEN Ativo = 1 THEN 1 ELSE 0 END) AS RegistrosAtivos
FROM [dbo].[CorrespondenteConvenio]
GROUP BY TipoConvenio
ORDER BY TipoConvenio;

