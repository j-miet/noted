namespace NotedApi.Features.Notes.Dtos;

public record CreateNoteRequest
(
    string Text = "",
    int X = 300,
    int Y = 200
);
