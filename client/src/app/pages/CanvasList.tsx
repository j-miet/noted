import { useEffect, useState } from "react";
import { apiRequest } from "../requestApi";

import "./CanvasList.css";
import type { Canvas } from "../../types/pages";

export default function CanvasPage() {
  const [canvases, setCanvases] = useState<Canvas[]>([]);

  const getCanvases = async () => {
    try {
      const data = await apiRequest("/api/canvas");

      setCanvases(data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    getCanvases();
  }, []);

  return (
    <div className="canvaslist-page">
      <h3>My canvases</h3>
      <ul className="canvaslist-list">
        {canvases.map((canvas) => {
          if (!canvas) return null;

          return (
            <li key={canvas.id} className="canvaslist-item">
              {canvas.name}
            </li>
          );
        })}
      </ul>
    </div>
  );
}
