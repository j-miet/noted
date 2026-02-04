namespace NotedApi.Features.Notes;

public record NoteResponse(
    int Id,
    int CanvasId,
    string Text,
    int X,
    int Y
);
