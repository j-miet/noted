import type { CanvasObject } from "../types/types";
import { apiRequest } from "./clientApi";

/**
 * API for canvas listing page's requests
 */
export const canvasesApi = {
  GetAllCanvases(page: number, limit: number) {
    return apiRequest(`/api/canvas/page=${page}&canvaslimit=${limit}`, {
      method: "GET",
    });
  },

  GetCanvasById(canvasId: number) {
    return apiRequest(`/api/canvas/${canvasId}`, {
      method: "GET",
    });
  },

  CreateCanvas(name: string) {
    return apiRequest(`/api/canvas`, {
      method: "POST",
      body: JSON.stringify({
        name,
      }),
    });
  },

  UpdateCanvasName(canvas: CanvasObject) {
    return apiRequest(`/api/canvas/${canvas.id}/name`, {
      method: "PATCH",
      body: JSON.stringify({
        name: canvas.name,
      }),
    });
  },

  DeleteCanvas(canvasId: number) {
    return apiRequest(`/api/canvas/${canvasId}`, {
      method: "DELETE",
    });
  },
};
