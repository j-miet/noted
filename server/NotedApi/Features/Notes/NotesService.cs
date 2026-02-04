using Microsoft.EntityFrameworkCore;

using NotedApi.Infrastructure.Data;
using NotedApi.Exceptions;
using NotedApi.Features.Notes;

public class NotesService : INotesService
{
    private readonly NotedDbContext _db;

    public NotesService(NotedDbContext db)
    {
        _db = db;
    }

    public async Task<List<NoteResponse>> GetNotesByCanvasIdAsync(int canvasId)
    {
        return await _db.Notes
            .Where(n => n.CanvasId == canvasId)
            .Select(n => new NoteResponse
            (
                n.Id,
                n.CanvasId,
                n.Text,
                n.X,
                n.Y
            ))
            .ToListAsync();
    }

    public async Task<NoteResponse> GetNoteByIdAsync(int noteId)
    {
        NoteResponse note = await _db.Notes
            .Where(n => n.Id == noteId)
            .Select(n => new NoteResponse
            (
                n.Id,
                n.CanvasId,
                n.Text,
                n.X,
                n.Y
            ))
            .FirstOrDefaultAsync() ?? throw new NotFoundException("Note not found");

        return note;
    }

    public async Task<NoteResponse> CreateNoteAsync(int canvasId, CreateNoteRequest req)
    {
        Note note = new()
        {
            CanvasId = canvasId,
            Text = req.Text,
            X = req.X,
            Y = req.Y
        };

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        return new NoteResponse(
            note.Id,
            note.CanvasId,
            note.Text,
            note.X,
            note.Y
        );
    }

    public async Task UpdateNoteTextAsync(int noteId, UpdateNoteTextRequest req)
    {
        Note updateNote = await _db.Notes.FindAsync(noteId) ?? throw new NotFoundException("Note not found");

        updateNote.Text = req.Text;
        await _db.SaveChangesAsync();
    }

    public async Task UpdateNotePositionAsync(int noteId, UpdateNotePosRequest req)
    {
        Note updateNote = await _db.Notes.FindAsync(noteId) ?? throw new NotFoundException("Note not found");

        updateNote.X = req.X;
        updateNote.Y = req.Y;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteNoteAsync(int noteId)
    {
        Note? deletedNote = await _db.Notes.FindAsync(noteId) ?? throw new NotFoundException("Note not found");

        _db.Notes.Remove(deletedNote);
        await _db.SaveChangesAsync();
    }
}