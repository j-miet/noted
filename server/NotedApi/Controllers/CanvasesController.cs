using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NotedApi.Data;
using NotedApi.Features.Canvases;

namespace NotedApi.Controllers;

[ApiController]
[Route("api/canvas")]
public class CanvasesController(NotedDbContext db) : ControllerBase
{
    private readonly NotedDbContext _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAllCanvases()
    {
        List<Canvas> canvases = await _db.Canvases.ToListAsync();

        return Ok(canvases);
    }

    [HttpGet("{canvasId}")]
    public async Task<IActionResult> GetCanvasById(int canvasId)
    {
        Canvas? canvas = await _db.Canvases.Where(c => c.Id == canvasId).FirstOrDefaultAsync();

        if (canvas == null) return NotFound();

        return Ok(canvas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCanvas(Canvas canvas)
    {
        _db.Canvases.Add(canvas);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCanvasById), new { id = canvas.Id }, canvas);
    }

    [HttpPatch("{canvasId}/name")]
    public async Task<IActionResult> UpdateCanvasName(int canvasId, Canvas canvas)
    {
        Canvas? updateCanvas = await _db.Canvases.FindAsync(canvasId);

        if (updateCanvas == null) return NotFound();

        updateCanvas.Name = canvas.Name;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{canvasId}")]
    public async Task<IActionResult> DeleteCanvas(int canvasId)
    {
        Canvas? deletedCanvas = await _db.Canvases.FindAsync(canvasId);

        if (deletedCanvas == null) return NotFound();

        _db.Canvases.Remove(deletedCanvas);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
