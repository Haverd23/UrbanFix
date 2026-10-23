# **UrbanFix - API de Gestão de Chamados de Reparos Urbanos**

A **UrbanFix** é uma API desenvolvida em ASP.NET Core para a gestão de chamados de reparos urbanos. Permite registrar chamados, consultar ocorrências e atualizar seus status, além de oferecer recursos para filtrar e organizar os chamados cadastrados.

---

## 📦 Funcionalidades

- **Criação de chamados**: Permite o envio de novos chamados de reparo com a opção de anexar imagens.
- **Consulta de chamados**: Possibilita consultar chamados cadastrados por diferentes critérios.
- **Atualização de status de chamados**: Permite a mudança de status de um chamado específico.
- **Busca de endereço**: Integração com a API ViaCEP para buscar informações de endereço com base no CEP fornecido.

---

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 10.0**: Framework utilizado para o desenvolvimento da API.
- **Entity Framework Core**: ORM utilizado para o mapeamento e a persistência de dados.
- **Redis**: Utilizado para armazenamento em cache, visando melhorar o desempenho das consultas.
- **Docker**: Utilizado para a conteinerização da aplicação e de seus serviços.
- **ViaCEP**: API externa utilizada para consultar informações de endereço a partir do CEP.

---

## 🧠 Como Funciona

### 1. Criação de Chamados

A API permite registrar chamados de reparos urbanos por meio dos seguintes campos obrigatórios:

- **Tipo (int)**: Tipo de problema reportado:
  - `0` = Lixo
  - `1` = Buraco
  - `2` = Energia
  - `3` = Outros
- **Descricao (string)**: Descrição do problema reportado.
- **CEP (string)**: CEP do local da ocorrência, utilizado para consultar as informações de endereço por meio da API ViaCEP.
- **Numero (string)**: Número do imóvel ou local da ocorrência.

### 2. Busca de Endereço

A partir do CEP informado, a API consulta o serviço ViaCEP para obter informações de endereço, como:

- **Logradouro**: Rua, avenida ou outra via.
- **Bairro**: Bairro da ocorrência.
- **Cidade**: Cidade onde ocorreu o problema.
- **Estado**: Unidade federativa do endereço.

Essas informações são associadas ao chamado durante seu processamento.

### 3. Consulta de Chamados

A API permite consultar os chamados cadastrados utilizando filtros, paginação e ordenação.

**Filtros disponíveis:**

- **Tipo**: Filtra chamados pelo tipo de ocorrência (`Lixo`, `Buraco`, `Energia` ou `Outros`).
- **Bairro**: Filtra chamados pelo bairro da ocorrência.
- **Status**: Filtra chamados pelo status atual.

**Paginação:**

A consulta permite dividir os resultados em páginas por meio dos seguintes parâmetros:

- **Page (int)**: Número da página.
- **PageSize (int)**: Quantidade de registros por página.

**Ordenação:**

Os resultados podem ser ordenados pelos seguintes campos:

- `Tipo`: Tipo da ocorrência.
- `DataCriacao`: Data de criação do chamado.
- `Status`: Status do chamado.
- `Bairro`: Bairro da ocorrência.

O parâmetro `OrderDirection` define a direção da ordenação:

- `Asc`: Ordem crescente.
- `Desc`: Ordem decrescente.

**Cache:**

Os resultados das consultas são armazenados em cache utilizando Redis, com duração de 10 minutos. A chave do cache é gerada a partir dos parâmetros da consulta, permitindo armazenar resultados diferentes conforme os filtros, a paginação e a ordenação utilizados.

### 4. Finalização de Chamados

A API permite finalizar um chamado existente por meio de seu identificador (`Guid`).

Ao realizar a operação, o status do chamado é atualizado para `Finalizado`, e a alteração é persistida no banco de dados.

O identificador do chamado é retornado na resposta após a conclusão da operação.

## 📋 Definição dos Enums

### TipoDeProblema

Representa o tipo de problema reportado no chamado.

- `0` = **Lixo**
- `1` = **Buraco**
- `2` = **Energia**
- `3` = **Outros**

### TipoDeStatus

Representa o status atual do chamado.

- `0` = **Aberto**
- `1` = **Finalizado**

## 🏗️ Arquitetura e Recursos

- **CQRS (Command Query Responsibility Segregation)**: Separação das operações de escrita (*Commands*) das operações de leitura (*Queries*), utilizando implementações próprias de Commands, Queries e Handlers.
- **Paginação**: Divisão dos resultados das consultas em páginas, permitindo controlar a quantidade de registros retornados.
- **Filtros e ordenação**: Consulta de chamados por tipo, bairro e status, com ordenação por tipo, data de criação, status ou bairro.
- **Cache com Redis**: Armazenamento temporário dos resultados das consultas por 10 minutos, utilizando chaves geradas a partir dos filtros, da paginação e da ordenação.
- **Padronização de respostas**: Utilização de uma estrutura genérica `ApiResponse<T>`, com os campos `Sucesso`, `Mensagem` e `Dados`, para manter um formato consistente nas respostas da API.
- **Tratamento global de exceções**: Utilização de um middleware próprio para centralizar o tratamento de exceções e retornar respostas padronizadas em JSON. Exceções de domínio resultam em HTTP `400 Bad Request`, enquanto exceções não tratadas resultam em HTTP `500 Internal Server Error`.

## 📁 Estrutura do Projeto

A solução UrbanFix está organizada em projetos separados, com o objetivo de dividir as responsabilidades da aplicação.

```text
src/
├── UrbanFix.Api/
│   └── UrbanFix.Api.csproj
├── UrbanFix.Application/
│   └── UrbanFix.Application.csproj
├── UrbanFix.Core/
│   └── UrbanFix.Core.csproj
├── UrbanFix.Data/
│   └── UrbanFix.Data.csproj
└── UrbanFix.Domain/
    └── UrbanFix.Domain.csproj
```

### Projetos

- **UrbanFix.Api**: Camada de entrada da aplicação, responsável por expor os endpoints HTTP.
- **UrbanFix.Application**: Camada responsável pela orquestração dos casos de uso da aplicação.
- **UrbanFix.Core**: Projeto destinado a componentes centrais e abstrações compartilhadas.
- **UrbanFix.Data**: Camada relacionada ao acesso e à persistência dos dados.
- **UrbanFix.Domain**: Camada que representa o domínio do negócio, contendo entidades, regras e comportamentos de domínio.

## 🐳 Como Executar a Aplicação

### Pré-requisitos

- [Docker](https://www.docker.com/)
- Docker Compose
- Arquivo `.env` com as variáveis de ambiente necessárias

### Configuração das variáveis de ambiente

Crie um arquivo `.env` no mesmo diretório do `docker-compose.yml`:

```env
DB_SA_PASSWORD=SuaSenhaForteAqui
DEFAULT_CONNECTION=Server=sqlserver,1433;Database=UrbanFix;User Id=sa;Password=SuaSenhaForteAqui;TrustServerCertificate=True
REDIS_CONNECTION=redis:6379
```

**Importante:** substitua `SuaSenhaForteAqui` por uma senha forte. O valor de `DB_SA_PASSWORD` deve corresponder à senha utilizada na connection string `DEFAULT_CONNECTION`.

### Inicialização

No diretório que contém o arquivo `docker-compose.yml`, execute:

```bash
docker compose up -d
```

O Docker Compose iniciará os serviços necessários:

- **API:** disponibiliza os endpoints HTTP da aplicação.
- **SQL Server:** banco de dados utilizado para persistência.
- **Redis:** serviço de cache.
- **Migrações:** aplica as migrações do banco de dados antes da inicialização da API.

A API depende da conclusão bem-sucedida do serviço de migração para iniciar.

### Acessando a API

Após a inicialização, a API estará disponível em:

```text
http://localhost:8080
```

Os dados do SQL Server e do Redis são armazenados em volumes Docker, permitindo sua persistência após a remoção dos containers.

## 🔌 Endpoints da API

A API disponibiliza endpoints para criar chamados, listar chamados com filtros e paginação, e finalizar chamados.

### Criar chamado

**`POST /Chamados`**

Cria um novo chamado. O endereço é obtido a partir do CEP informado, utilizando a API ViaCEP.

**Corpo da requisição:**

```json
{
  "tipo": 0,
  "descricao": "Buraco na rua",
  "cep": "37500-000",
  "numero": "123"
}
```

| Campo | Tipo | Descrição |
|---|---|---|
| `tipo` | `int` | Tipo do chamado: `0` Lixo, `1` Buraco, `2` Energia, `3` Outros. |
| `descricao` | `string` | Descrição do problema. |
| `cep` | `string` | CEP do endereço do chamado. |
| `numero` | `string` | Número do imóvel ou local. |

**Resposta:** `201 Created`

O campo `dados` contém o identificador (`Guid`) do chamado criado.

### Listar chamados

**`GET /Chamados`**

Lista os chamados cadastrados, permitindo aplicar filtros, ordenação e paginação.

**Parâmetros de consulta:**

| Parâmetro | Tipo | Descrição |
|---|---|---|
| `Tipo` | `string` | Filtra pelo tipo do chamado. |
| `Bairro` | `string` | Filtra pelo bairro. |
| `Status` | `string` | Filtra pelo status do chamado. |
| `Page` | `int` | Número da página. O padrão é `1` quando o valor é menor ou igual a zero. |
| `PageSize` | `int` | Quantidade de itens por página. O padrão é `10` quando o valor é menor ou igual a zero, com limite máximo de `50`. |
| `OrderBy` | `enum` | Campo de ordenação: `Tipo`, `DataCriacao`, `Status` ou `Bairro`. |
| `OrderDirection` | `enum` | Direção da ordenação: `Asc` ou `Desc`. |

**Exemplo de requisição:**

```http
GET /Chamados?Tipo=Buraco&Bairro=Centro&Status=Aberto&Page=1&PageSize=10&OrderBy=DataCriacao&OrderDirection=Desc
```

**Resposta:** `200 OK`

O campo `dados` contém os resultados paginados, com a seguinte estrutura:

```json
{
  "sucesso": true,
  "mensagem": null,
  "dados": {
    "items": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "tipo": "Buraco",
        "descricao": "Buraco na rua",
        "dataCriacao": "2026-01-01T10:00:00",
        "status": "Aberto",
        "cep": "37500-000",
        "numero": "123",
        "logradouro": "Rua Exemplo",
        "bairro": "Centro",
        "cidade": "Itajubá",
        "estado": "MG"
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalItems": 1,
    "totalPages": 1
  }
}
```

Os valores apresentados são ilustrativos. `totalItems` representa a quantidade total de registros encontrados, e `totalPages` indica o número total de páginas.

### Finalizar chamado

**`PUT /Chamados/{id}`**

Finaliza um chamado existente, utilizando seu identificador na rota.

**Exemplo de requisição:**

```http
PUT /Chamados/00000000-0000-0000-0000-000000000000
```

**Resposta:** `200 OK`

O campo `dados` contém o identificador (`Guid`) do chamado finalizado.

### Formato das respostas

As respostas seguem o padrão `ApiResponse<T>`, composto pelos seguintes campos:

| Campo | Tipo | Descrição |
|---|---|---|
| `sucesso` | `bool` | Indica se a operação foi bem-sucedida. |
| `mensagem` | `string` ou `null` | Mensagem associada à resposta. |
| `dados` | variável | Dados retornados pela operação. |