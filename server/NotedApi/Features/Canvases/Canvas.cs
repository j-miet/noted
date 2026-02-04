using NotedApi.Features.Notes;

namespace NotedApi.Features.Canvases;

/// <summary>
/// Canvas model
/// </summary>
public class Canvas
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Note> Notes { get; set; } = [];
}
