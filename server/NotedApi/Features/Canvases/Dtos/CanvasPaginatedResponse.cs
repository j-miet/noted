namespace NotedApi.Features.Canvases.Dtos;

public record CanvasPaginatedResponse(
    List<CanvasResponse> Canvases,
    int TotalCount,
    int PageIndex,
    int TotalPages
);