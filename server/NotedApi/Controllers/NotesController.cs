using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotedApi.Data;

namespace NotedApi.Controllers;

[ApiController]
[Route("api")]
public class NotesController : ControllerBase
{
    private readonly NotedDbContext _db;

    public NotesController(NotedDbContext db)
    {
        _db = db;
    }

    [HttpGet("[Controller]/{id}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var notes = await _db.Notes.Where(n => n.Id == id).FirstOrDefaultAsync();

        return Ok(notes);
    }
}
