import "./Topbar.css";
import Breadcrumps from "./components/Breadcrumps";
import CanvasName from "./components/CanvasName";

export default function Topbar() {
  return (
    <header className="topbar">
      <Breadcrumps></Breadcrumps>
      <CanvasName></CanvasName>
    </header>
  );
}
