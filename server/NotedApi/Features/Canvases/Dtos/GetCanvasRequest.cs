namespace NotedApi.Features.Canvases.Dtos;

public record GetCanvasRequest(
    int CurrentPage,
    int CanvasLimit
);