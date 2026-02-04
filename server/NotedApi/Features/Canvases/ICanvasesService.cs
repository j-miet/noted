namespace NotedApi.Features.Canvases;

public interface ICanvasesService
{
    Task<List<CanvasResponse>> GetAllCanvasesAsync();
    Task<CanvasResponse> GetCanvasByIdAsync(int canvasId);
    Task<CanvasResponse> CreateCanvasAsync(CreateCanvasRequest req);
    Task UpdateCanvasNameAsync(int canvasId, UpdateCanvasRequest Name);
    Task DeleteCanvasAsync(int canvasId);
}