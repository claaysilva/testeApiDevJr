namespace testeApi.Models
{
  /// <summary>
  /// Entidade que representa um post retornado pela API JSONPlaceholder e persistido no banco.
  /// </summary>
  public class Post
  {
    /// <summary>
    /// Identificador do post (também usado como Id de origem da API externa).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador do usuário autor do post (campo de referência simples, sem relacionamento nesta versão).
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Título do post.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo do post.
    /// </summary>
    public string Body { get; set; } = string.Empty;
  }
}
