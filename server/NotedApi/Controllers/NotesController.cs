using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotedApi.Data;
using NotedApi.Features.Notes;

namespace NotedApi.Controllers;

// TODO clean up controllers by moving their logic under Features/Notes/NotesService.cs
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
        List<NoteDTO> notes = await _db.Notes
            .Where(n => n.CanvasId == canvasId)
            .Select(n => new NoteDTO
            {
                Id = n.Id,
                CanvasId = n.CanvasId,
                Text = n.Text,
                X = n.X,
                Y = n.Y
            })
            .ToListAsync();

        return Ok(notes);
    }

    [HttpGet("notes/{noteId}")]
    public async Task<IActionResult> GetNoteById(int noteId)
    {
        NoteDTO? note = await _db.Notes
            .Where(n => n.Id == noteId)
            .Select(n => new NoteDTO
            {
                Id = n.Id,
                CanvasId = n.CanvasId,
                Text = n.Text,
                X = n.X,
                Y = n.Y
            })
            .FirstOrDefaultAsync();

        if (note == null) return NotFound();

        return Ok(note);
    }

    [HttpPost("canvas/{canvasId}/notes")]
    public async Task<IActionResult> CreateNote(int canvasId, CreateNoteRequest dto)
    {
        Note note = new()
        {
            CanvasId = canvasId,
            Text = dto.Text,
            X = dto.X,
            Y = dto.Y
        };

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetNoteById),
            new { id = canvasId },
            new NoteDTO
            {
                Id = note.Id,
                CanvasId = note.CanvasId,
                Text = note.Text,
                X = note.X,
                Y = note.Y
            }
        );
    }

    [HttpPatch("notes/{noteId}/text")]
    public async Task<IActionResult> UpdateNoteText(int noteId, UpdateNoteTextRequest dto)
    {
        Note? updateNote = await _db.Notes.FindAsync(noteId);
        if (updateNote == null) return NotFound();

        updateNote.Text = dto.Text;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("notes/{noteId}/position")]
    public async Task<IActionResult> UpdateNotePosition(int noteId, UpdateNotePosRequest dto)
    {
        Note? updateNote = await _db.Notes.FindAsync(noteId);
        if (updateNote == null) return NotFound();

        updateNote.X = dto.X;
        updateNote.Y = dto.Y;
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
