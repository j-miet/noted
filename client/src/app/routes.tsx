import { createBrowserRouter, Navigate } from "react-router";

import App from "./App";
import CanvasPage from "./pages/Canvas";
import { canvasesApi } from "../api/canvasesApi";
import CanvasesPage from "./pages/Canvases";

const router = createBrowserRouter([
  {
    path: "/",
    Component: App,
    children: [
      { index: true, element: <Navigate to="/canvases" replace /> },
      {
        path: "canvases",
        Component: CanvasesPage,
        loader: () => {
          return { name: "canvases" };
        },
      },
      {
        path: "canvases/:canvasId",
        Component: CanvasPage,
        loader: async ({ params }) => {
          const canvas = await canvasesApi.GetCanvasById(
            Number(params.canvasId),
          );
          return { name: canvas.name, id: canvas.id };
        },
      },
    ],
  },
]);

export default router;
