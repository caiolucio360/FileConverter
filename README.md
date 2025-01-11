# FileConverter API

Uma API REST em .NET 8 para conversão de arquivos, com suporte para upload de arquivos, validação e autenticação por API Key.

## Descrição

Este projeto demonstra a criação de uma API REST robusta para conversão de arquivos, utilizando o framework .NET 8. Ele oferece:

*   **Upload de Arquivos:** Permite o upload de arquivos para conversão.
*   **Conversão para Base64:** Converte arquivos para o formato Base64.
*   **Validação de Entrada:** Utiliza FluentValidation para garantir a integridade dos dados de entrada.
*   **Autenticação por API Key:** Protege os endpoints com autenticação baseada em API Key.
*   **Documentação Swagger:** Gera automaticamente a documentação da API usando o Swagger/OpenAPI.
*   **CORS:** Configurado para permitir requisições de origens específicas.
*   **Health Checks:** Monitora a saúde da API.
*   **Tratamento de Erros:** Retorno de erros consistentes e informativos.

## Tecnologias Utilizadas

*   .NET 8
*   ASP.NET Core Web API
*   FluentValidation
*   Swashbuckle.AspNetCore (Swagger/OpenAPI)
*   System.Text.Json
*   HealthChecks.AspNetCore

## Pré-requisitos

*   .NET 8 SDK instalado.
*   Um editor de código (como Visual Studio, Visual Studio Code ou Rider).

## Como Executar

1.  Clone o repositório:

    ```bash
    git clone [URL inválido removido]
    ```

2.  Navegue até o diretório do projeto:

    ```bash
    cd FileConverter
    ```

3.  Restaure as dependências:

    ```bash
    dotnet restore
    ```

4.  Execute o projeto:

    ```bash
    dotnet run
    ```

5.  Acesse a documentação do Swagger em: `https://localhost:<porta>/swagger` (substitua `<porta>` pela porta que sua aplicação está usando, geralmente 5000 para HTTP ou 5001 para HTTPS).

## Configuração

A configuração da API é feita através do arquivo `appsettings.json`.

*   **`AllowedOrigins` (CORS):** Define as origens permitidas para acessar a API.

    ```json
    "AllowedOrigins": [
      "http://localhost:4200", // Exemplo: seu frontend local
      "[https://seu-dominio.com](https://seu-dominio.com)" // Seu domínio em produção
    ]
    ```

*   **`ApiSettings:ApiKey` (Autenticação):** Define a chave de API que deve ser enviada no header `x-api-key`.

    ```json
    "ApiSettings": {
      "ApiKey": "SUA_CHAVE_API_AQUI"
    }
    ```

## Endpoints

A API possui o seguinte endpoint:

*   `POST api/v1/converter/base64`: Converte um arquivo para Base64. Requer um arquivo no formato `multipart/form-data` e uma API Key no header `x-api-key`.

## Uso

Para utilizar o endpoint de conversão, envie uma requisição `POST` para `api/v1/converter/base64` com:

*   Um arquivo no formato `multipart/form-data` no corpo da requisição.
*   Um header `x-api-key` contendo a API Key configurada.

**Exemplo de requisição (usando `curl`):**

```bash
curl -X POST \
  'https://localhost:<porta>/api/v1/converter/base64' \
  -H 'x-api-key: SUA_CHAVE_API_AQUI' \
  -H 'Content-Type: multipart/form-data' \
  -F 'Files=@/caminho/para/seu/arquivo.txt'
