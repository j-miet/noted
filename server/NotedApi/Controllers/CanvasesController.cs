using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NotedApi.Data;
using NotedApi.Features.Canvases;

namespace NotedApi.Controllers;

// TODO clean up controllers by moving their logic under Features/Notes/CanvasesService.cs
[ApiController]
[Route("api/canvas")]
public class CanvasesController(NotedDbContext db) : ControllerBase
{
    private readonly NotedDbContext _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAllCanvases()
    {
        List<CanvasDTO> canvases = await _db.Canvases
            .Select(c => new CanvasDTO
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return Ok(canvases);
    }

    [HttpGet("{canvasId}")]
    public async Task<IActionResult> GetCanvasById(int canvasId)
    {
        CanvasDTO? canvas = await _db.Canvases
            .Where(c => c.Id == canvasId)
            .Select(c => new CanvasDTO
            {
                Id = c.Id,
                Name = c.Name
            })
            .FirstOrDefaultAsync();

        if (canvas == null) return NotFound();

        return Ok(canvas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCanvas(CreateCanvasRequest dto)
    {
        Canvas canvas = new()
        {
            Name = dto.Name
        };

        _db.Canvases.Add(canvas);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCanvasById),
            new { id = canvas.Id },
            new CanvasDTO
            {
                Id = canvas.Id,
                Name = canvas.Name
            }
        );
    }

    [HttpPatch("{canvasId}/name")]
    public async Task<IActionResult> UpdateCanvasName(int canvasId, UpdateCanvasRequest dto)
    {
        Canvas? updateCanvas = await _db.Canvases.FindAsync(canvasId);

        if (updateCanvas == null) return NotFound();

        updateCanvas.Name = dto.Name;
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
