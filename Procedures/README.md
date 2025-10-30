# Scripts SQL - SIICO

Este diretório contém os scripts SQL necessários para configurar o banco de dados e popular com dados de exemplo.

## Ordem de Execução

Execute os scripts na seguinte ordem:

### 1. Criar Tabela
Execute primeiro o script `01_CriarTabelaCorrespondenteConvenio.sql` para criar a estrutura da tabela.

```sql
-- Executa: 01_CriarTabelaCorrespondenteConvenio.sql
```

### 2. Criar Procedure
Execute o script `pr_BuscarDados.sql` para criar a stored procedure de busca.

```sql
-- Executa: pr_BuscarDados.sql
```

### 3. Inserir Dados de Exemplo
Execute o script `02_InserirDadosExemplo.sql` para popular a tabela com dados de exemplo.

```sql
-- Executa: 02_InserirDadosExemplo.sql
```

## Estrutura da Tabela

A tabela `CorrespondenteConvenio` possui os seguintes campos:

- `Id` (INT, IDENTITY) - Chave primária
- `CorrespondenteId` (INT) - ID do correspondente
- `NumConvenio` (INT) - Número do convênio (único)
- `NumCnpj` (VARCHAR(14)) - CNPJ da empresa
- `NoEmpresa` (NVARCHAR(200)) - Nome da empresa
- `NoFantasia` (NVARCHAR(200)) - Nome fantasia
- `TipoConvenio` (INT) - Tipo do convênio (1-4)
- `DataCriacao` (DATETIME2) - Data de criação
- `Ativo` (BIT) - Status ativo/inativo

## Procedure: pr_BuscarDados

A procedure `pr_BuscarDados` realiza a busca paginada de correspondentes convênios com base nos parâmetros do HateoasService:

### Parâmetros de Entrada:
- `@TipoConvenio` (INT) - Tipo do convênio a ser filtrado
- `@Pagina` (INT) - Número da página
- `@Limite` (INT) - Quantidade de registros por página

### Parâmetro de Saída:
- `@TotalRegistros` (INT OUTPUT) - Total de registros encontrados

### Retorno:
Retorna os registros paginados que correspondem ao filtro de tipo de convênio e status ativo.

## Dados de Exemplo

O script `02_InserirDadosExemplo.sql` insere 1000 registros de exemplo por padrão, distribuídos entre os tipos de convênio 1 a 4.

Para alterar a quantidade de registros, modifique a variável `@TotalRegistros` no início do script.

