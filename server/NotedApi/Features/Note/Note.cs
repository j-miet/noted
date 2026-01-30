namespace NotedApi.Features.Note;

public class Note
{
    public int Id { get; set; }
    public int CanvasId { get; set; }
    public string? Text { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}
