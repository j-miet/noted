using NotedApi.Features.Canvases.Dtos;

namespace NotedApi.Features.Canvases;

public interface ICanvasesService
{
    Task<CanvasPaginatedResponse> GetAllCanvasesAsync(GetCanvasRequest req);
    Task<CanvasResponse> GetCanvasByIdAsync(int canvasId);
    Task<CanvasResponse> CreateCanvasAsync(CreateCanvasRequest req);
    Task UpdateCanvasNameAsync(int canvasId, UpdateCanvasRequest req);
    Task DeleteCanvasAsync(int canvasId);
}