namespace NotedApi.Features.Notes;

public class CreateNoteRequest
{
    public string Text { get; set; } = "";
    public int X { get; set; } = 300;
    public int Y { get; set; } = 200;
}
