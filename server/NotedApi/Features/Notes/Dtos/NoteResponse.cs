namespace NotedApi.Features.Notes.Dtos;

public record NoteResponse(
    int Id,
    int CanvasId,
    string Text,
    int X,
    int Y
);
