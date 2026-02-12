import { useEffect, useState } from "react";

import "./Canvases.css";
import type { Canvas } from "../../types/pages.ts";
import { canvasesApi } from "../../api/canvasesApi.ts";
import { nameCheck } from "../../utils/inputCheck.ts";
import { Link } from "react-router";

export default function HomePage() {
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

  const getAllCanvases = async (): Promise<void> => {
    try {
      const data = await canvasesApi.GetAllCanvases(currentPage, 7);

      setCanvases(data.canvases);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error(error);
    }
  };

  const createCanvas = async (): Promise<void> => {
    try {
      const name = nameCheck(prompt("Name your canvas: "));
      if (name === "") return;

      await canvasesApi.CreateCanvas(name);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  const updateCanvasName = async (canvas: Canvas): Promise<void> => {
    try {
      const name = nameCheck(prompt(`Rename canvas '${canvas.name}':`));
      if (name === "") return;

      canvas.name = name;
      await canvasesApi.UpdateCanvasName(canvas);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  const deleteCanvas = async (canvas: Canvas): Promise<void> => {
    try {
      if (!confirm(`Delete canvas '${canvas.name}'?`)) return;

      await canvasesApi.DeleteCanvas(canvas.id);

      getAllCanvases();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="canvases-page">
      <div className="canvas-card">
        <div className="ui">
          <h3>My canvases</h3>
          <hr></hr>
          <ul>
            {canvases.map((canvas) => {
              if (!canvas) return null;

              return (
                <li key={canvas.id} className="list-item">
                  <Link to={`/canvases/${canvas.id}`} className="list-link">
                    {canvas.name}
                  </Link>

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
    </div>
  );
}
