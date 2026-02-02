using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NotedApi.Data;

namespace NotedApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CanvasesController : ControllerBase
{
    private readonly NotedDbContext _db;

    public CanvasesController(NotedDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var canvases = await _db.Canvases.ToListAsync();

        return Ok(canvases);
    }
}
