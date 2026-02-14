import { useEffect, useState } from "react";
import { useLoaderData } from "react-router";

import Sidebar from "../../features/Sidebar/Sidebar";
import Topbar from "../../features/Topbar/Topbar";
import "./Canvas.css";
import type { NoteObject } from "../../types/types.ts";
import { canvasApi } from "../../api/canvasApi";
import Note from "../../features/Note/Note";

export default function CanvasPage() {
  const [notes, setNotes] = useState<NoteObject[]>([]);
  const urlData = useLoaderData();

  useEffect(() => {
    // eslint-disable-next-line react-hooks/immutability
    getAllNotesByCanvasId(urlData.id);
  }, [urlData.id]);

  const getAllNotesByCanvasId = async (canvasId: number): Promise<void> => {
    try {
      const data = await canvasApi.GetAllNotesByCanvasId(canvasId);

      setNotes(data);
    } catch (error) {
      console.error(error);
    }
  };

  const createNote = async (): Promise<void> => {
    try {
      await canvasApi.CreateNote(urlData.id);

      getAllNotesByCanvasId(urlData.id);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="canvas-page">
      <Topbar></Topbar>
      <main className="canvas-area">
        <Sidebar create={createNote}></Sidebar>
        {notes.map((note) => {
          if (!note) return null;
          return <Note key={note.id} noteData={note}></Note>;
        })}
      </main>
    </div>
  );
}
