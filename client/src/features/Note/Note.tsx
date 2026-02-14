import "./Note.css";

import type { noteProps } from "../../types/props";
import { useEffect, useRef, useState } from "react";
import { canvasApi } from "../../api/canvasApi";

export default function Note({ noteData }: noteProps) {
  const [position, SetPosition] = useState({ x: noteData.x, y: noteData.y });
  //const [text, SetText] = useState(noteData.text);
  const [dragging, SetDragging] = useState(false);
  const noteRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    const note = noteRef.current;
    if (note == null) return;
    note.style.left = position.x + "px";
    note.style.top = position.y + "px";
  }, [position]);

  const handleMouseDown = () => {
    SetDragging(true);
  };

  const handleMouseUp = (): void => {
    SetDragging(false);
    canvasApi.UpdateNotePosition(noteData.id, position);
  };

  const handleMouseMove = (e: React.MouseEvent<HTMLDivElement>): void => {
    if (!dragging || !noteRef.current) return;

    const rect = noteRef.current.getBoundingClientRect();

    // TODO add offset here; otherwise dragging is not possible
    // check the ChatGPT advice, especially using window.addEventListeners
    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;

    SetPosition({ x, y });
  };

  return (
    <div
      ref={noteRef}
      className="canvas-note"
      onMouseDown={handleMouseDown}
      onMouseMove={handleMouseMove}
      onMouseUp={handleMouseUp}
    >
      Drag me!
    </div>
  );
}
