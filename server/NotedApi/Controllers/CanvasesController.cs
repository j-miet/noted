using Microsoft.AspNetCore.Mvc;

using NotedApi.Features.Canvases;
using NotedApi.Features.Canvases.Dtos;

namespace NotedApi.Controllers;

[ApiController]
[Route("api/canvas")]
public class CanvasesController : ControllerBase
{
    private readonly ICanvasesService _canvases;

    public CanvasesController(ICanvasesService canvases)
    {
        _canvases = canvases;
    }

    [HttpGet("page={page}&canvaslimit={limit}")]
    public async Task<IActionResult> GetAllCanvases(int page, int limit)
    {
        GetCanvasRequest req = new(page, limit);
        return Ok(await _canvases.GetAllCanvasesAsync(req));
    }

    [HttpGet("{canvasId}")]
    public async Task<IActionResult> GetCanvasById(int canvasId)
    {
        return Ok(await _canvases.GetCanvasByIdAsync(canvasId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCanvas(CreateCanvasRequest req)
    {
        return Ok(await _canvases.CreateCanvasAsync(req));
    }

    [HttpPatch("{canvasId}/name")]
    public async Task<IActionResult> UpdateCanvasName(int canvasId, UpdateCanvasRequest req)
    {
        await _canvases.UpdateCanvasNameAsync(canvasId, req);
        return NoContent();
    }

    [HttpDelete("{canvasId}")]
    public async Task<IActionResult> DeleteCanvas(int canvasId)
    {
        await _canvases.DeleteCanvasAsync(canvasId);
        return NoContent();
    }
}
