import Sidebar from "../../features/Sidebar/Sidebar";
import Topbar from "../../features/Topbar/Topbar";
import "./Canvas.css";

export default function CanvasPage() {
  return (
    <div className="canvas-page">
      <Topbar></Topbar>
      <Sidebar></Sidebar>
      <section className="canvas-area"></section>
    </div>
  );
}
