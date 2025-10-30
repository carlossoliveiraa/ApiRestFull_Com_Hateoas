-- Script para criar a tabela CorrespondenteConvenio
-- Execute este script antes de executar o script de inserção de dados

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorrespondenteConvenio]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CorrespondenteConvenio] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [CorrespondenteId] INT NOT NULL,
        [NumConvenio] INT NOT NULL,
        [NumCnpj] VARCHAR(14) NOT NULL,
        [NoEmpresa] NVARCHAR(200) NOT NULL,
        [NoFantasia] NVARCHAR(200) NOT NULL,
        [TipoConvenio] INT NOT NULL,
        [DataCriacao] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [Ativo] BIT NOT NULL DEFAULT 1,
        CONSTRAINT [UK_CorrespondenteConvenio_NumConvenio] UNIQUE ([NumConvenio])
    );

    CREATE INDEX [IX_CorrespondenteConvenio_TipoConvenio] ON [dbo].[CorrespondenteConvenio] ([TipoConvenio]);
    CREATE INDEX [IX_CorrespondenteConvenio_Ativo] ON [dbo].[CorrespondenteConvenio] ([Ativo]);
    
    PRINT 'Tabela CorrespondenteConvenio criada com sucesso!';
END
ELSE
BEGIN
    PRINT 'Tabela CorrespondenteConvenio já existe.';
END;


