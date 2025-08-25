using Microsoft.AspNetCore.Mvc;
using testeApi.Data;
using testeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace testeApi.Controllers
{
  /// <summary>
  /// Controlador responsável por importar e listar Posts.
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class PostsController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Injeta o DbContext. Um HttpClient simples é instanciado para consumo da API externa.
    /// Em cenários avançados, prefira IHttpClientFactory para melhor resiliência.
    /// </summary>
    public PostsController(AppDbContext context)
    {
      _context = context;
      _httpClient = new HttpClient();
    }

    // GET: api/posts/fetch
    /// <summary>
    /// Busca posts do JSONPlaceholder e salva no banco, evitando duplicidade por Id.
    /// </summary>
    [HttpGet("fetch")]
    public async Task<IActionResult> FetchAndSavePosts()
    {
      var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
      if (!response.IsSuccessStatusCode)
        return StatusCode((int)response.StatusCode, "Erro ao buscar dados da API externa.");

      var posts = await response.Content.ReadFromJsonAsync<List<Post>>();

      if (posts == null) return BadRequest("Nenhum post encontrado.");

      foreach (var post in posts)
      {
        if (!_context.Posts.Any(p => p.Id == post.Id))
        {
          _context.Posts.Add(post);
        }
      }

      await _context.SaveChangesAsync();
      return Ok("Posts importados e salvos com sucesso.");
    }

    // GET: api/posts
    /// <summary>
    /// Lista todos os posts persistidos no banco.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
      return await _context.Posts.ToListAsync();
    }
  }
}
