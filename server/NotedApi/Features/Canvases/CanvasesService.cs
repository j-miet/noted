using Microsoft.EntityFrameworkCore;

using NotedApi.Infrastructure.Data;
using NotedApi.Exceptions;
using NotedApi.Utils;
using NotedApi.Features.Canvases.Dtos;


namespace NotedApi.Features.Canvases;

public class CanvasesService : ICanvasesService
{
    private readonly NotedDbContext _db;

    public CanvasesService(NotedDbContext db)
    {
        _db = db;
    }

    public async Task<CanvasPaginatedResponse> GetAllCanvasesAsync(GetCanvasRequest req)
    {
        List<CanvasResponse> canvases = await _db.Canvases
            .OrderBy(r => r.Name)
            .Skip((req.CurrentPage - 1) * req.CanvasLimit)
            .Take(req.CanvasLimit)
            .Select(r => new CanvasResponse(
                r.Id,
                r.Name
            ))
            .ToListAsync();

        int count = await _db.Canvases.CountAsync();
        int totalPages = (int)Math.Ceiling(Math.Max(1, count) / (double)req.CanvasLimit);

        return new CanvasPaginatedResponse(canvases, count, req.CurrentPage, totalPages);
    }

    public async Task<CanvasResponse> GetCanvasByIdAsync(int canvasId)
    {
        CanvasResponse canvas = await _db.Canvases
            .Where(c => c.Id == canvasId)
            .Select(r => new CanvasResponse(
                r.Id,
                r.Name
            ))
            .FirstOrDefaultAsync() ?? throw new NotFoundException("Canvas not found");

        return canvas;
    }

    public async Task<CanvasResponse> CreateCanvasAsync(CreateCanvasRequest req)
    {
        if (CheckUtils.NameCheck(req.Name) == "") throw new InvalidCanvasException("Invalid canvas name");

        Canvas canvas = new()
        {
            Name = req.Name
        };

        _db.Canvases.Add(canvas);
        await _db.SaveChangesAsync();

        return new CanvasResponse(
            canvas.Id,
            canvas.Name
        );
    }

    public async Task UpdateCanvasNameAsync(int canvasId, UpdateCanvasRequest req)
    {
        if (CheckUtils.NameCheck(req.Name) == "") throw new InvalidCanvasException("Invalid canvas name");

        Canvas updateCanvas = await _db.Canvases.FindAsync(canvasId)
            ?? throw new NotFoundException("Canvas not found");

        updateCanvas.Name = req.Name;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteCanvasAsync(int canvasId)
    {
        Canvas? deletedCanvas = await _db.Canvases.FindAsync(canvasId)
            ?? throw new NotFoundException("Canvas not found");

        _db.Canvases.Remove(deletedCanvas);
        await _db.SaveChangesAsync();
    }
}