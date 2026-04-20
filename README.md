# GestaoBancariaRest

API REST de gestão bancária desenvolvida em ASP.NET Core, com Entity Framework Core e SQLite. Projeto desenvolvido como estudo de caso para comparação de desempenho entre REST e gRPC.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger (OpenAPI)

## Arquitetura

O projeto segue arquitetura em camadas:

```
GestaoBancaria_01_2026.sln
├── Apresentacao/   → Controllers, DTOs, configuração da API
├── Dominio/        → Regras de negócio
├── Entidades/      → Modelos e enums
└── Repositorio/    → Acesso a dados, migrations, DataContext
```

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [dotnet-ef](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet)

```bash
dotnet tool install --global dotnet-ef --version 8.0.0
```

## Como rodar

**1. Clone o repositório**
```bash
git clone https://github.com/jeanvitorvieira/GestaoBancariaRest.git
cd GestaoBancariaRest
```

**2. Aplique as migrations**
```bash
dotnet ef database update --project Repositorio/Repositorio.csproj --startup-project Apresentacao/Apresentacao.csproj
```

**3. Rode a aplicação**
```bash
dotnet run --project Apresentacao/Apresentacao.csproj
```

**4. Acesse o Swagger**
```
http://localhost:5000/swagger
```

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/api/contasbancarias` | Criar conta |
| `GET` | `/api/contasbancarias` | Listar contas |
| `GET` | `/api/contasbancarias/{id}` | Buscar conta por ID |
| `GET` | `/api/contasbancarias/{id}/extrato` | Extrato com movimentos |
| `DELETE` | `/api/contasbancarias/{id}` | Deletar conta |
| `POST` | `/api/contasbancarias/movimentos` | Inserir movimento (Pix) |

## Testes de carga

Os scripts de benchmark estão na pasta `Benchmarks/` e utilizam [k6](https://k6.io).

```bash
# Popular o banco com 1.000 movimentos
k6 run Benchmarks/seed_movimentos.js

# Testar listagem de contas
k6 run Benchmarks/rest_contas.js

# Testar extrato (payload pesado)
k6 run Benchmarks/rest_extrato.js
```

## Artigo

Este projeto é parte de um estudo comparativo entre REST e gRPC publicado no Dev.to:

[Análise acerca do Framework gRPC: Comparações e Desempenhos](https://dev.to/jeanvitorvieira/analise-acerca-do-framework-grpc-google-remote-procedure-call-comparacoes-e-desempenhos-5amd)

O projeto gRPC equivalente está disponível em [GestaoBancariaGrpc](https://github.com/jeanvitorvieira/GestaoBancariaGrpc).

## Autores

- Bruno de Moraes Supriano
- Jean Vitor Vieira

Centro Universitário SATC — Engenharia de Software — 2026
