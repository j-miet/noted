import { createBrowserRouter } from "react-router";

import HomePage from "./pages/Canvases";
import App from "./App";
import CanvasPage from "./pages/Canvas";

const router = createBrowserRouter([
  {
    path: "canvases",
    Component: App,
    children: [
      { index: true, Component: HomePage },
      { path: ":canvasId", Component: CanvasPage },
    ],
  },
  {
    path: "*",
    Component: App,
  },
]);

export default router;
