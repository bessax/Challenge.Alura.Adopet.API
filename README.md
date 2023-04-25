<h2 align="center"> Challenge backend - Alura Adopet</h2>
<h3 align="center">Projeto desenvolvido na plataforma .NET e linguagem C#. 😄</h3>

## 📚 Sobre o projeto

O desafio proposto nesta 6ª edição do Challenge da Alura é o desenvolvimento de uma Rest API, para realização do crud de Tutor, Pet e Abrigo.

## 📝 Conteúdo

- [Sobre o projeto](#-sobre-o-projeto)

## Configuração do ambiente

### 📋 Pré-requisitos

- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### 🎲 Configuração do banco de dados

A configuração do banco de dados é feita através do arquivo appsettings.json, que fica na raiz do projeto. O arquivo já está configurado para o banco de dados **Sql Server** local, mas caso queira utilizar outro banco de dados, basta alterar a string de conexão.

```json
"ConnectionStrings": {
    "AdopetAPI": "Server=(localdb)\\mssqllocaldb;Database=AdopetV2;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

## 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/) - Linguagem
- [.NET](https://docs.microsoft.com/pt-br/dotnet/) - Framework
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/) - ORM
- [Swagger](https://swagger.io/) - Documentação da API

<!-- Author -->

### ✒️ Autor(as/es)

- **André Bessa** - _Desenvolvedor_ - [bessax](https://github.com/bessax)
