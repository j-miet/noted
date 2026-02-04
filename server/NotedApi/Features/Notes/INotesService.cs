namespace NotedApi.Features.Notes;

public interface INotesService
{
    Task<List<NoteResponse>> GetNotesByCanvasIdAsync(int canvasId);
    Task<NoteResponse> GetNoteByIdAsync(int noteId);
    Task<NoteResponse> CreateNoteAsync(int canvasId, CreateNoteRequest req);
    Task UpdateNoteTextAsync(int noteId, UpdateNoteTextRequest req);
    Task UpdateNotePositionAsync(int noteId, UpdateNotePosRequest req);
    Task DeleteNoteAsync(int noteId);
}