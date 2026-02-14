import type { NoteObject } from "./types";

export interface noteProps {
  noteData: NoteObject;
}

export interface sidebarProps {
  create: () => Promise<void>;
}
