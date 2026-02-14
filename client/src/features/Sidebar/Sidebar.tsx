import type { sidebarProps } from "../../types/props";
import "./Sidebar.css";

// sidebar for canvas controls
export default function Sidebar({ create }: sidebarProps) {
  return (
    <aside className="sidebar">
      <button onClick={() => create()}>Note</button>
    </aside>
  );
}
