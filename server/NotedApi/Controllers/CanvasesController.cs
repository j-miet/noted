using Microsoft.AspNetCore.Mvc;
using NotedApi.Features.Notes;

namespace NotedApi.Controllers;

[ApiController]
[Route("api/canvases")]
public class CanvasesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        // test data here; database support will be added soon
        var canvas = new
        {
            Id = 1,
            Notes = new List<Note>{
                new() { Id = 1, CanvasId = 1, Text = "text", X = 100, Y = 650},
                new() { Id = 2, CanvasId = 1 }}
        };

        return Ok(canvas);
    }
}
