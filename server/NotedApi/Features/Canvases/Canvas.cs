using NotedApi.Features.Notes;

namespace NotedApi.Features.Canvases;

public class Canvas
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public List<Note> Notes { get; set; } = [];
}
