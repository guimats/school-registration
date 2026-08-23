# SchoolRegistration API - Sistema de Controle de Matrículas

API REST desenvolvida em **.NET Framework 4.8** com **ASP.NET Web API 2** para gerenciamento de alunos, turmas e matrículas escolares. O projeto utiliza **Dapper** com queries SQL manuais, controle transacional explícito e arquitetura em camadas.

---

## Tecnologias e Bibliotecas

- **Framework:** .NET Framework 4.8 (ASP.NET Web API 2)
- **Banco de Dados:** SQL Server
- **Acesso a Dados:** Dapper (Consultas e comandos SQL manuais)
- **Injeção de Dependência:** Simple Injector
- **Validação:** FluentValidation
- **Documentação Interativa:** Swagger (Swashbuckle 5.6.0)
- **Testes Unitários:** xUnit, Moq e Shouldly

---

## Arquitetura do Projeto

A solução foi estruturada no padrão de camadas (*Layered Architecture*), mantendo a Controller responsável apenas pelo transporte HTTP:

- **`SchoolRegistration.API`:** Endpoints REST, filtros globais de exceção (`CustomExceptionFilterAttribute`), documentação Swagger e inicialização do container IoC.
- **`SchoolRegistration.Service`:** Regras de negócio, orquestração de chamadas, transações e validações via FluentValidation.
- **`SchoolRegistration.Infrastructure`:** Repositórios com Dapper, gerenciamento de conexões e execução de scripts SQL/transações.
- **`SchoolRegistration.Domain`:** Entidades, DTOs de Request/Response, Interfaces e Exceções customizadas (`ValidationException`, `BusinessRuleException`).
- **`SchoolRegistration.Tests`:** Testes unitários com mocks cobrindo os fluxos e regras de negócio da matrícula.

---

## Como Executar a Aplicação

### 1. Pré-requisitos
- Visual Studio 2019 / 2022 com o workload de desenvolvimento para desktop/web .NET.
- SQL Server (Express, LocalDB ou Developer Edition).

### 2. Configuração do Banco de Dados
1. Abra o SQL Server Management Studio (SSMS) ou o Azure Data Studio.
2. Execute o arquivo **`script-banco.sql`** incluído na raiz do repositório para criar as tabelas (`Aluno`, `Turma`, `Matricula`) e a carga inicial de dados.

### 3. Configuração da Connection String
A connection string padrão está configurada para **Localhost com Windows Authentication** no arquivo `Web.config` do projeto `SchoolRegistration.API`:

```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Server=localhost;Database=TesteEscola;Integrated Security=True;TrustServerCertificate=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Executando a API

1. Abra o arquivo `SchoolRegistration.sln` no Visual Studio.
2. Defina o projeto **`SchoolRegistration.API`** como o projeto de inicialização (Set as Startup Project).
3. Pressione **F5** (ou **Ctrl + F5**).
4. O navegador abrirá a documentação interativa do Swagger na URL:

```text
http://localhost:{PORTA}/swagger
```

## Endpoints da API

### Alunos (`/api/alunos`)
* **`GET /api/alunos?name={nome}&page={pagina}&pageSize={tamanho}`**: Listagem paginada de alunos ativos/inativos, informando o total de registros via DTO com paginação (`OFFSET / FETCH NEXT`).
* **`GET /api/alunos/{id}`**: Retorna os dados do aluno por ID. Retorna `404 Not Found` se não existir.
* **`POST /api/alunos`**: Cadastra um novo aluno. Retorna `201 Created` com a URI e o DTO gerado.
* **`PUT /api/alunos/{id}`**: Atualiza os dados de um aluno. Retorna `204 NoContent`.
* **`DELETE /api/alunos/{id}`**: Realiza a exclusão lógica alterando o campo `Ativo = 0`. Retorna `204 NoContent`.

### Turmas (`/api/turmas`)
* **`GET /api/turmas`**: Lista todas as turmas cadastradas exibindo as vagas restantes.

### Matrículas (`/api/matriculas`)
* **`POST /api/matriculas`**: Realiza a matrícula de um aluno em uma turma.
  * **Validações e Regras de Negócio:**
    * Aluno precisa existir e estar ativo (`Ativo = 1`).
    * Turma precisa existir e conter `VagasDisponiveis > 0`.
    * Aluno não pode estar matriculado duas vezes na mesma turma.

### Relatórios (`/api/relatorios`)
* **`GET /api/relatorios/alunos-por-turma`**: Retorna relatório agregado via SQL (`GROUP BY` e `JOIN`) com o nome da turma, quantidade de alunos matriculados e vagas restantes.

---

## Tratamento de Erros e Status HTTP

O projeto utiliza um filtro global (`CustomExceptionFilterAttribute`) que padroniza as respostas de erro da API:

| Status Code | Cenário |
| :--- | :--- |
| **`200 OK` / `201 Created`** | Operação executada com sucesso. |
| **`204 NoContent`** | Atualizações e exclusões sem corpo de retorno. |
| **`400 Bad Request`** | Parâmetros obrigatórios ausentes, tipos inválidos ou falhas de validação do FluentValidation (`ValidationException`). |
| **`404 Not Found`** | Recurso não localizado pelo ID informado. |
| **`409 Conflict`** | Violações de regras de negócio (aluno já matriculado, turma sem vagas disponíveis, aluno inativo) via `BusinessRuleException`. |
| **`500 Internal Server Error`** | Erros inesperados ou falhas não tratadas de infraestrutura. |

---

## Executando os Testes Unitários

1. No Visual Studio, abra o menu superior **Test** -> **Test Explorer**.
2. Clique em **Run All Tests** (`Ctrl + R, A`).
3. Os testes cobrem cenários como:
   * Tentativa de matrícula em turma sem vagas.
   * Tentativa de matrícula de aluno inativo.
   * Tentativa de matrícula duplicada.
