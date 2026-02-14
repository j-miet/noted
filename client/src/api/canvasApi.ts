import type { Position } from "../types/types";
import { apiRequest } from "./clientApi";

/**
 * API for canvas page's requests
 */
export const canvasApi = {
  GetAllNotesByCanvasId(canvasId: number) {
    return apiRequest(`/api/canvas/${canvasId}/notes`, {
      method: "GET",
    });
  },

  GetNoteById(noteId: number) {
    return apiRequest(`/api/notes/${noteId}`, {
      method: "GET",
    });
  },

  CreateNote(canvasId: number) {
    return apiRequest(`/api/canvas/${canvasId}/notes`, {
      method: "POST",
      body: JSON.stringify({}),
    });
  },

  UpdateNoteText(noteId: number, text: string) {
    return apiRequest(`/api/notes/${noteId}/text`, {
      method: "PATCH",
      body: JSON.stringify({
        text,
      }),
    });
  },

  UpdateNotePosition(noteId: number, position: Position) {
    return apiRequest(`/api/notes/${noteId}/position`, {
      method: "PATCH",
      body: JSON.stringify({
        ...position,
      }),
    });
  },

  DeleteNote(noteId: number) {
    return apiRequest(`/api/notes/${noteId}`, {
      method: "DELETE",
    });
  },
};
