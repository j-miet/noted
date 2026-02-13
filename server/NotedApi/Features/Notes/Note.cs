using NotedApi.Features.Canvases;

namespace NotedApi.Features.Notes;

/// <summary>
/// Note model for database
/// </summary>
public class Note
{
    public int Id { get; set; }
    public int CanvasId { get; set; }
    public Canvas Canvas { get; set; } = null!;
    public string Text { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
}
