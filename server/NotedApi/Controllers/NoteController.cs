using Microsoft.AspNetCore.Mvc;

namespace NotedApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNote(int id)
    {
        var note = new
        {
            Id = id,
            CanvasId = 2,
            Text = "test",
            X = 100,
            Y = 100
        };

        return Ok(note);
    }
}
