# testeApiDevJr

API .NET 8 que consome posts da API pública JSONPlaceholder e persiste em MySQL usando Entity Framework Core.

## Tecnologias

- .NET 8 (ASP.NET Core)
- Entity Framework Core 9 + Pomelo MySQL
- Swagger (Swashbuckle)

## Requisitos

- .NET SDK 8+
- MySQL 8+ em localhost

## Configuração

Edite a connection string em `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=testeApi;user=root;password=SUA_SENHA;"
}
```

Opcionalmente, defina variável de ambiente para a senha:

- Windows PowerShell:
  - `$env:ConnectionStrings__DefaultConnection = "server=localhost;database=testeApi;user=root;password=SUA_SENHA;"`

## Executar

- Restaurar e rodar:

```
dotnet restore
 dotnet ef database update
 dotnet run
```

A API sobe em `https://localhost:7047` (ou porta do seu ambiente). Swagger disponível em `/swagger`.

## Endpoints

- `GET /api/posts/fetch` — Busca os posts do JSONPlaceholder e salva no banco, ignorando duplicados por Id.
- `GET /api/posts` — Lista todos os posts do banco.

## Migrações EF Core

- Criar migração: `dotnet ef migrations add NomeDaMigracao`
- Aplicar: `dotnet ef database update`
- Remover última: `dotnet ef migrations remove`
- Dropar banco: `dotnet ef database drop`

## Estrutura do projeto (principal)

- `Program.cs` — Configuração do pipeline e DbContext (MySQL)
- `Data/AppDbContext.cs` — DbContext
- `Models/Post.cs` — Entidade Post
- `Controllers/PostsController.cs` — Endpoints da API
- `Migrations/` — Migrações do EF

## Desenvolvimento

Commits já versionados em `main`:

- Configuração MySQL e controllers
- Modelo Post e DbContext
- Migração inicial para o banco `testeApi`

## Notas

- Em produção, prefira usar secrets/variáveis de ambiente para credenciais.
- Ajuste políticas de CORS se for consumir via front-end.
