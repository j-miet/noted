using Microsoft.AspNetCore.Mvc;

namespace NotedApi.Controllers;

[ApiController]
[Route("api")]
public class NotesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNote(int id)
    {
        // test data here; database will be added soon
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
