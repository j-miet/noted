export interface CanvasObject {
  id: number;
  name: string;
}

export interface NoteObject {
  id: number;
  canvasId: number;
  text: string;
  x: number;
  y: number;
}

export interface Position {
  x: number;
  y: number;
}
