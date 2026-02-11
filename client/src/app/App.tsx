import { Outlet } from "react-router";
import "./App.css";

export default function App() {
  return (
    <div className="noted-app">
      <Outlet />
    </div>
  );
}
