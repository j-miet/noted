namespace NotedApi.Features.Notes;

public class Note
{
    public int Id { get; set; }
    public int CanvasId { get; set; }
    public string Text { get; set; } = "";
    public int X { get; set; } = 300;
    public int Y { get; set; } = 200;
}
