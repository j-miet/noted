import { useEffect, useState } from "react";

import "./CanvasList.css";
import type { Canvas } from "../../types/pages";
import { canvaslistApi } from "../../api/canvaslistApi.ts";
import { nameCheck } from "../../utils/inputCheck.ts";

export default function CanvasPage() {
  const [canvases, setCanvases] = useState<Canvas[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  // load all canvases when page is refreshed, but not during rerenders
  useEffect(() => {
    getAllCanvases();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentPage]);

  useEffect(() => {
    if (currentPage > totalPages) {
      setCurrentPage(totalPages || 1);
    }
  }, [currentPage, totalPages]);

  const getAllCanvases = async () => {
    try {
      const data = await canvaslistApi.GetAllCanvases(currentPage, 7);

      setCanvases(data.canvases);
      setTotalPages(data.totalPages);
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

  return (
    <div className="canvaslist-page">
      <div className="canvas-ui">
        <h3>My canvases</h3>
        <hr></hr>
        <ul>
          {canvases.map((canvas) => {
            if (!canvas) return null;

            return (
              <li key={canvas.id} className="list-item">
                {canvas.name}

                <button
                  onClick={() => updateCanvasName(canvas)}
                  className="updatename"
                >
                  Rename
                </button>
                <button
                  onClick={() => deleteCanvas(canvas)}
                  className="deleteitem"
                >
                  X
                </button>
              </li>
            );
          })}
        </ul>
      </div>

      <div className="general">
        <hr></hr>
        <button onClick={createCanvas} className="additem">
          New Canvas
        </button>
      </div>

      <div className="pagination">
        <button
          disabled={currentPage <= 1}
          onClick={() => setCurrentPage((p) => Math.max(1, p - 1))}
          className="prev-button"
        >
          {"<-"}
        </button>
        <span>
          Page {currentPage} / {totalPages}
        </span>
        <button
          disabled={currentPage >= totalPages}
          onClick={() => setCurrentPage((p) => Math.min(totalPages, p + 1))}
          className="next-button"
        >
          {"->"}
        </button>
      </div>
    </div>
  );
}
