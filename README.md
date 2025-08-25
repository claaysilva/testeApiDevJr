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

### Documentação Swagger com comentários XML
- O projeto gera o arquivo XML de documentação automaticamente (csproj configurado).
- O Swagger inclui esses comentários na UI para endpoints e controllers.
- Acesse `https://localhost:xxxxx/swagger` (porta conforme seu ambiente) e veja descrições e summaries.

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

## Guia de arquitetura e manutenção

Camadas:

- Controllers: expõem endpoints HTTP e coordenam o fluxo.
- Data: `AppDbContext` configura o EF Core e mapeia entidades.
- Models: classes de domínio (por enquanto, `Post`).

Pontos de extensão:

- Adicionar validação: use DataAnnotations nos Models ou FluentValidation.
- Paginação/filtros: exponha parâmetros em `GET /api/posts` e aplique no `DbSet`.
- HttpClient: para produção, injete `IHttpClientFactory` via `AddHttpClient`.
- Logs: use `ILogger<T>` nos controllers.

Checklist de manutenção:

1. Alterou o modelo? Crie migração e atualize o banco.
2. Mudou a conexão? Atualize `appsettings.json` ou variáveis de ambiente.
3. Novos endpoints? Documente no Swagger (XML comments) e README.

## Troubleshooting

- Erro de porta HTTPS: defina a URL no `launchSettings.json` ou desabilite redireção durante testes locais.
- Tabela já existe ao aplicar migração: drope o banco (`dotnet ef database drop`) ou crie baseline.
- Conexão MySQL falha: verifique usuário/senha/host e se o MySQL está ouvindo em 3306.

## Desenvolvimento

Commits já versionados em `main`:

- Configuração MySQL e controllers
- Modelo Post e DbContext
- Migração inicial para o banco `testeApi`

## Notas

- Em produção, prefira usar secrets/variáveis de ambiente para credenciais.
- Ajuste políticas de CORS se for consumir via front-end.
