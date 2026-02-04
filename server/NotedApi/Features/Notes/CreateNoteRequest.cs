namespace NotedApi.Features.Notes;

public record CreateNoteRequest
(
    string Text = "",
    int X = 300,
    int Y = 200
);
