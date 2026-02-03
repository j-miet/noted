using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotedApi.Data;
using NotedApi.Features.Notes;

namespace NotedApi.Controllers;

[ApiController]
[Route("api")]
public class NotesController : ControllerBase
{
    private readonly NotedDbContext _db;

    public NotesController(NotedDbContext db)
    {
        _db = db;
    }

    [HttpGet("canvas/{canvasId}/notes")]
    public async Task<IActionResult> GetNotesByCanvasId(int canvasId)
    {
        List<Note> notes = await _db.Notes.Where(n => n.CanvasId == canvasId).ToListAsync();

        return Ok(notes);
    }

    [HttpGet("notes/{noteId}")]
    public async Task<IActionResult> GetNoteById(int noteId)
    {
        Note? note = await _db.Notes.Where(n => n.Id == noteId).FirstOrDefaultAsync();
        if (note == null) return NotFound();

        return Ok(note);
    }

    [HttpPost("canvas/{canvasId}/notes")]
    public async Task<IActionResult> CreateNote(int canvasId, Note note)
    {
        // this looks a bit ugly, but will soon get replaced by a DTO anyway
        note.CanvasId = canvasId;
        note.Canvas = null!;

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNoteById), new { id = canvasId }, note);
    }

    [HttpPatch("notes/{noteId}/text")]
    public async Task<IActionResult> UpdateNoteText(int noteId, Note note)
    {
        Note? updateNote = await _db.Notes.FindAsync(noteId);
        if (updateNote == null) return NotFound();

        updateNote.Text = note.Text;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("notes/{noteId}/position")]
    public async Task<IActionResult> UpdateNotePosition(int noteId, Note note)
    {
        Note? updateNote = await _db.Notes.FindAsync(noteId);
        if (updateNote == null) return NotFound();

        updateNote.X = note.X;
        updateNote.Y = note.Y;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("notes/{noteId}")]
    public async Task<IActionResult> DeleteNote(int noteId)
    {
        Note? deletedNote = await _db.Notes.FindAsync(noteId);
        if (deletedNote == null) return NotFound();

        _db.Notes.Remove(deletedNote);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
