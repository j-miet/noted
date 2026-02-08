using Microsoft.AspNetCore.Mvc;

using NotedApi.Features.Notes;

namespace NotedApi.Controllers;

[ApiController]
[Route("api")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notes;

    public NotesController(INotesService notes)
    {
        _notes = notes;
    }

    [HttpGet("canvas/{canvasId}/notes")]
    public async Task<IActionResult> GetNotesByCanvasId(int canvasId)
    {
        return Ok(await _notes.GetNotesByCanvasIdAsync(canvasId));
    }

    [HttpGet("notes/{noteId}")]
    public async Task<IActionResult> GetNoteById(int noteId)
    {
        return Ok(await _notes.GetNoteByIdAsync(noteId));
    }

    [HttpPost("canvas/{canvasId}/notes")]
    public async Task<IActionResult> CreateNote(int canvasId, CreateNoteRequest req)
    {
        return Ok(await _notes.CreateNoteAsync(canvasId, req));
    }

    [HttpPatch("notes/{noteId}/text")]
    public async Task<IActionResult> UpdateNoteText(int noteId, UpdateNoteTextRequest req)
    {
        await _notes.UpdateNoteTextAsync(noteId, req);
        return NoContent();
    }

    [HttpPatch("notes/{noteId}/position")]
    public async Task<IActionResult> UpdateNotePosition(int noteId, UpdateNotePosRequest req)
    {
        await _notes.UpdateNotePositionAsync(noteId, req);
        return NoContent();
    }

    [HttpDelete("notes/{noteId}")]
    public async Task<IActionResult> DeleteNote(int noteId)
    {
        await _notes.DeleteNoteAsync(noteId);
        return NoContent();
    }
}
