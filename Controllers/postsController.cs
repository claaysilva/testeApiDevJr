using Microsoft.AspNetCore.Mvc;
using testeApi.Data;
using testeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace testeApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PostsController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public PostsController(AppDbContext context)
    {
      _context = context;
      _httpClient = new HttpClient();
    }

    // GET: api/posts/fetch
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
      return await _context.Posts.ToListAsync();
    }
  }
}
