using Microsoft.EntityFrameworkCore;
using testeApi.Models;

namespace testeApi.Data
{
  /// <summary>
  /// DbContext principal da aplicação. Mapeia entidades para tabelas MySQL via EF Core (Pomelo).
  /// </summary>
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Conjunto de Posts persistidos no banco (tabela "Posts").
    /// </summary>
    public DbSet<Post> Posts { get; set; }

    // Dica de manutenção: Configure Fluent API aqui caso precise alterar nomes de colunas, índices ou relacionamentos.
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //   modelBuilder.Entity<Post>().ToTable("Posts");
    // }
  }
}
