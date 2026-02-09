import { useEffect, useState } from "react";

import "./CanvasList.css";
import type { Canvas } from "../../types/pages";
import { canvaslistApi } from "../../api/canvaslistApi.ts";
import { nameCheck } from "../../utils/inputCheck.ts";

export default function CanvasPage() {
  const [canvases, setCanvases] = useState<Canvas[]>([]);

  const getAllCanvases = async () => {
    try {
      const data = await canvaslistApi.GetAllCanvases();

      setCanvases(data);
    } catch (error) {
      console.error(error);
    }
  };

  const createCanvas = async () => {
    try {
      const name = nameCheck(prompt("Name your canvas: "));
      if (name === "") return;

      await canvaslistApi.CreateCanvas(name);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  const updateCanvasName = async (canvas: Canvas) => {
    try {
      const name = nameCheck(prompt(`Rename canvas '${canvas.name}':`));
      if (name === "") return;

      canvas.name = name;
      await canvaslistApi.UpdateCanvasName(canvas);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  const deleteCanvas = async (canvas: Canvas) => {
    try {
      if (!confirm(`Delete canvas '${canvas.name}'?`)) return;

      await canvaslistApi.DeleteCanvas(canvas.id);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  // load all canvases when page is refreshed, but not during rerenders
  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    getAllCanvases();
  }, []);

  return (
    <div className="canvaslist-page">
      <h3>My canvases</h3>
      <hr></hr>
      <ul className="canvaslist-list">
        {canvases.map((canvas) => {
          if (!canvas) return null;

          return (
            <li key={canvas.id} className="canvaslist-item">
              {canvas.name}

              <button
                onClick={() => updateCanvasName(canvas)}
                className="canvaslist-updatename"
              >
                Rename
              </button>
              <button
                onClick={() => deleteCanvas(canvas)}
                className="canvaslist-deleteitem"
              >
                X
              </button>
            </li>
          );
        })}
      </ul>
      <button onClick={createCanvas} className="canvaslist-additem">
        New Canvas
      </button>
    </div>
  );
}
