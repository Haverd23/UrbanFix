# **UrbanFix - API de Gestão de Chamados de Reparos Urbanos**

A API **UrbanFix** é uma solução desenvolvida em ASP.NET Core que permite a gestão de chamados de reparos urbanos. Através dessa API, é possível criar, atualizar, consultar e excluir chamados, além de fornecer diversas formas de filtrar chamados com base em critérios como status, tipo e endereços obtidos a partir de CEPs.

---

## **📦 Funcionalidades**

- **Criação de chamados**: Permite o envio de novos chamados de reparo com a opção de anexar imagens.
- **Consulta de chamados**: Possibilita a busca por chamados por ID, status, tipo e mais antigos.
- **Atualização de status de chamados**: Permite a mudança de status de um chamado específico.
- **Exclusão de chamados**: Permite a remoção de um chamado.
- **Busca de endereço**: Integração com a API ViaCEP para buscar informações de endereço com base no CEP fornecido.

---

## **🚀 Tecnologias Utilizadas**

- **ASP.NET Core 8.0**: Framework utilizado para o desenvolvimento da API.
- **Entity Framework Core**: Para a persistência de dados.
- **Swagger**: Para documentação e testes interativos da API.
- **API ViaCEP**: Para busca de endereços a partir do CEP.

---

## **🧠 Como Funciona**

1. **Criação de Chamado**: O usuário envia um chamado de reparo preenchendo os campos obrigatórios:
   - **Tipo**: Um valor representando o tipo do problema reportado. O valor é um dos seguintes:
     - `0` para **Lixo**
     - `1` para **Buraco**
     - `2` para **Falta de Luz**
   - **Descrição**: Uma breve descrição do problema reportado.
   - **CEP**: O código postal onde o reparo é necessário. A partir do CEP, a API integra-se ao ViaCEP para buscar informações de endereço, como logradouro, bairro, cidade e estado.
   - **Número**: O número do local onde o reparo é necessário.
   - **Base64Imagem** (opcional): Uma imagem do local do problema, convertida para Base64 e enviada no formato de string.

2. **Busca de Endereço**: Com base no **CEP** fornecido, a API consulta o serviço ViaCEP para obter as informações de endereço, como:
   - **Logradouro**: Rua, avenida, etc.
   - **Bairro**: O bairro do local.
   - **Cidade**: A cidade do local.
   - **Estado**: O estado onde o local está localizado.
   
   Essas informações de endereço são associadas ao chamado automaticamente.

3. **Persistência e Resposta**: O sistema processa o chamado e o salva no banco de dados. O ID do chamado recém-criado é retornado como resposta.

4. **Consulta de Chamados**: É possível consultar chamados já existentes filtrando por:
   - **ID**: Busca um chamado específico.
   - **Status**: Filtro de chamados de acordo com seu status (Em Aberto, Em Andamento ou Finalizado).
   - **Tipo**: Filtro de chamados por tipo de problema (Lixo, Buraco, Falta de Luz).
   - **Antigos**: Filtro de chamados mais antigos por tipo ou status.

5. **Atualização de Status**: O status de um chamado pode ser alterado, permitindo transitar entre os seguintes status:
   - **Em Aberto (0)**
   - **Em Andamento (1)**
   - **Finalizado (2)**

6. **Exclusão de Chamados**: Chamados podem ser removidos permanentemente pelo seu ID.

---

## **📋 Definição dos Enums**

### **Tipo**:
Representa o tipo de problema reportado no chamado.
- `0` = **Lixo**
- `1` = **Buraco**
- `2` = **Falta de Luz**

### **Status**:
Representa o status do chamado.
- `0` = **Em Aberto**
- `1` = **Em Andamento**
- `2` = **Finalizado**

---

## **Exemplo de DTOs (Data Transfer Objects)**

## `CriarChamadoDTO`
```csharp
public class CriarChamadoDTO
{
    public int Tipo { get; set; }  // Tipo do problema: 0 = Lixo, 1 = Buraco, 2 = Falta de Luz
    public string Descricao { get; set; }
    public string CEP { get; set; }
    public string Numero { get; set; }
    public string? Base64Imagem { get; set; }  // Imagem opcional em Base64
}
```

## `EnderecoDTO`
```csharp
public class EnderecoDTO
{
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}
```

---

# 🔧 Como Executar Localmente

## Pré-requisitos
- .NET SDK 8.0 ou superior  
- SQL Server ou outro banco de dados configurado  
- Connection String para o banco de dados (configurar no arquivo `appsettings.json`)

## Passos

### Clone o repositório:
```bash
git clone https://github.com/Haverd23/UrbanFix.git
```

### Navegue até a pasta do projeto:
```bash
cd urbanfix-api/src/UrbanFix.WebApi
```

### Configure sua chave do ViaCep no arquivo `appsettings.json`.

### Execute as migrações para atualizar o banco de dados:
```bash
dotnet ef database update
```

Esse comando aplica todas as migrações pendentes ao banco de dados, garantindo que ele esteja atualizado com o esquema mais recente.

### Execute o projeto:
```bash
dotnet run
```

Agora a API estará rodando localmente e pronta para ser utilizada!

---

# ⚙️ Endpoints da API

## 1. Criar Chamado (POST)

- **Endpoint:** `/api/chamado`  
- **Método:** `POST`  
- **Descrição:** Cria um novo chamado de serviço público.

### Parâmetros:
- `Tipo (int)` - Tipo do problema (0 = Lixo, 1 = Buraco, 2 = Falta de Luz)  
- `Descricao (string)` - Descrição detalhada do problema  
- `CEP (string)` - CEP do local do problema  
- `Numero (string)` - Número do local do problema  
- `Base64Imagem (string, opcional)` - Imagem do problema em formato base64

### Exemplo de Request:
```json
POST /api/chamado
{
  "Tipo": 1,
  "Descricao": "Buraco grande na rua",
  "CEP": "12345-678",
  "Numero": "123",
  "Base64Imagem": "data:image/png;base64,iVBORw0..."
}
```

### Exemplo de Response:
```json
{
  "Tipo": 1,
  "Descricao": "Buraco grande na rua",
  "CEP": "12345-678",
  "Numero": "123",
  "Base64Imagem": "data:image/png;base64,iVBORw0...",
  "DataCriacao": "2025-04-16T12:00:00Z"
}
```

---

## 2. Obter Chamado por ID (GET)

- **Endpoint:** `/api/chamado/{id}`  
- **Método:** `GET`  
- **Descrição:** Obtém os detalhes de um chamado específico pelo ID.

### Exemplo de Request:
```http
GET /api/chamado/{id}
```

### Exemplo de Response:
```json
{
  "Descricao": "Buraco grande na rua",
  "CEP": "12345-678",
  "Numero": "123",
  "Tipo": "Buraco",
  "Status": "Em Aberto",
  "DataCriacao": "2025-04-16T12:00:00Z",
  "Endereco": {
    "Logradouro": "Rua Exemplo",
    "Bairro": "Centro",
    "Cidade": "São Paulo",
    "Estado": "SP"
  }
}
```

---

## 3. Atualizar Status do Chamado (PUT)

- **Endpoint:** `/api/chamado/{id}/status`  
- **Método:** `PUT`  
- **Descrição:** Atualiza o status de um chamado.

### Parâmetros:
- `id (Guid)` - ID do chamado a ser atualizado  
- `Status (int)` - Novo status do chamado (0 = Em Aberto, 1 = Em Andamento, 2 = Finalizado)

### Exemplo de Request:
```json
PUT /api/chamado/{id}/status
{
  "Status": 1
}
```

### Exemplo de Response:
```json
{
  "status": "204 No Content"
}
```

---

## 4. Remover Chamado (DELETE)

- **Endpoint:** `/api/chamado/{id}`  
- **Método:** `DELETE`  
- **Descrição:** Exclui um chamado pelo ID.

### Exemplo de Request:
```http
DELETE /api/chamado/{id}
```

### Exemplo de Response:
```json
{
  "status": "204 No Content"
}
```

---

## 5. Obter Todos os Chamados (GET)

- **Endpoint:** `/api/chamado`  
- **Método:** `GET`  
- **Descrição:** Obtém todos os chamados registrados.

### Exemplo de Request:
```http
GET /api/chamado
```

### Exemplo de Response:
```json
[
  {
    "Descricao": "Buraco grande na rua",
    "CEP": "12345-678",
    "Numero": "123",
    "Tipo": "Buraco",
    "Status": "Em Aberto",
    "DataCriacao": "2025-04-16T12:00:00Z",
    "Endereco": {
      "Logradouro": "Rua Exemplo",
      "Bairro": "Centro",
      "Cidade": "São Paulo",
      "Estado": "SP"
    }
  },
  {
    "Descricao": "Lixo acumulado na calçada",
    "CEP": "98765-432",
    "Numero": "456",
    "Tipo": "Lixo",
    "Status": "Em Andamento",
    "DataCriacao": "2025-04-16T12:30:00Z",
    "Endereco": {
      "Logradouro": "Rua Exemplo 2",
      "Bairro": "Vila Nova",
      "Cidade": "São Paulo",
      "Estado": "SP"
    }
  }
]
