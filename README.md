# 🚍 Ônibus Express API

API REST desenvolvida em **.NET 8** para gerenciamento de viagens de ônibus, permitindo o cadastro de rotas, viagens, passageiros e reservas de assentos.

O projeto foi desenvolvido seguindo boas práticas de arquitetura, princípios SOLID e separação de responsabilidades, servindo tanto como estudo quanto como base para aplicações corporativas.

---

# 📋 Funcionalidades

* Cadastro de Rotas
* Cadastro de Viagens
* Cadastro de Passageiros
* Realização de Reservas
* Validação de disponibilidade de assentos
* Tratamento global de exceções
* Validação de requisições utilizando FluentValidation
* Respostas padronizadas utilizando ProblemDetails (RFC 7807)
* Persistência em SQL Server
* Containerização utilizando Docker

---

# 🏛 Arquitetura

O projeto foi estruturado utilizando uma arquitetura em camadas.

```
src
│
├── OnibusExpress.Api
│
├── OnibusExpress.Application
│
├── OnibusExpress.Domain
│
└── OnibusExpress.Infrastructure
```

## API

Responsável pela exposição dos endpoints REST.

Contém:

* Controllers
* Middlewares
* Configurações
* Dependency Injection
* Swagger

---

## Application

Camada responsável pelos casos de uso da aplicação.

Contém:

* Services
* DTOs
* Validators
* Interfaces
* Regras de aplicação

---

## Domain

Responsável pelas regras de negócio.

Contém:

* Entities
* Enums
* Interfaces
* Exceptions
* Regras de domínio

---

## Infrastructure

Responsável pelo acesso a recursos externos.

Contém:

* Entity Framework Core
* Repositórios
* SQL Server
* Configuração do DbContext
* Migrations

---

# 🛠 Tecnologias

* .NET 8
* ASP.NET Core
* Entity Framework Core
* SQL Server
* Docker
* Docker Compose
* FluentValidation
* Swagger / OpenAPI

---

# 📦 Pré-requisitos

Antes de executar o projeto é necessário possuir instalado:

* .NET SDK 8
* Docker Desktop
* Git

---

# 🚀 Executando com Docker

Clone o repositório

```bash
git clone https://github.com/seuusuario/OnibusExpress.git

cd OnibusExpress
```

Suba os containers

```bash
docker compose up --build
```

ou em background

```bash
docker compose up -d --build
```

A aplicação ficará disponível em

```
http://localhost:8080
```

---

# 🗄 Banco de Dados

O projeto utiliza SQL Server executando em container Docker.

Porta padrão:

```
1433
```

Credenciais padrão:

| Campo    | Valor           |
| -------- | --------------- |
| Server   | localhost       |
| Porta    | 1433            |
| Database | OnibusExpressDb |
| Usuário  | sa              |
| Senha    | SuaSenha@123    |

Connection String:

```text
Server=localhost,1433;
Database=OnibusExpressDb;
User Id=sa;
Password=SuaSenha@123;
TrustServerCertificate=True;
Encrypt=False;
```

---

# ▶ Executando sem Docker

Restaurar pacotes

```bash
dotnet restore
```

Compilar

```bash
dotnet build
```

Executar

```bash
dotnet run --project src/OnibusExpress.Api
```

---

# 📚 Swagger

Após iniciar a aplicação, acesse:

```
http://localhost:8080/swagger
```

Toda documentação da API estará disponível pela interface do Swagger.

---

# 🧪 Testes

Executar todos os testes

```bash
dotnet test
```

---

# 📂 Estrutura do Projeto

```
OnibusExpress

├── src
│   ├── OnibusExpress.Api
│   ├── OnibusExpress.Application
│   ├── OnibusExpress.Domain
│   └── OnibusExpress.Infrastructure
│
├── tests
│
├── docker-compose.yml
├── Dockerfile
└── README.md
```

---

# 📌 Principais Regras de Negócio

## Rotas

* Cadastro de origem e destino.
* Uma rota pode possuir diversas viagens.

---

## Viagens

* Associadas a uma rota.
* Possuem quantidade máxima de assentos.
* Controlam assentos disponíveis.

---

## Passageiros

* Cadastro de passageiros.
* CPF único.
* Informações para identificação do cliente.

---

## Reservas

Uma reserva somente poderá ser realizada quando:

* existir uma viagem;
* existir um passageiro;
* houver assentos disponíveis;
* o assento informado ainda não estiver ocupado.

Caso alguma regra seja violada, a API retorna um **ProblemDetails** padronizado.

---

# ⚠ Tratamento de Erros

A API utiliza um middleware global para captura de exceções.

As respostas seguem o padrão RFC 7807.

Exemplo:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Um ou mais erros de validação ocorreram.",
  "status": 400,
  "detail": "Verifique os campos informados.",
  "instance": "/api/reservas",
  "errors": {
    "NumeroAssento": [
      "O número do assento informado não está disponível."
    ]
  }
}
```

---

# 🔍 Validações

Todas as validações são realizadas utilizando **FluentValidation**.

Exemplos:

* Campos obrigatórios
* CPF válido
* E-mail válido
* Quantidade de assentos
* Disponibilidade de assentos
* Regras de negócio

---

# 📈 Próximas Melhorias

* Autenticação JWT
* Cache
* CI/CD
* Testes de integração
* Testes de carga

---

# 👨‍💻 Autor

**Lucas Rodrigues Maximiano**

Desenvolvedor .NET com foco em arquitetura de software, APIs REST, microsserviços e soluções escaláveis utilizando tecnologias Microsoft.
