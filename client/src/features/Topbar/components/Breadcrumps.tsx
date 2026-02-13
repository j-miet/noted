import { Link, useLoaderData } from "react-router";
import "./Breadcrumps.css";

/**
 * Simple breadcrumps placaholder
 *
 * Doesn't utilize React router's handle object because there aren't many child routes. Instead customized breadcrumbs
 * are needed if/when canvases can refer to other canvases in the future.
 *
 * In other words, if canvas1 contains a link pointing to canvas2, breadcrumbs become
 *    canvases -> canvas1 -> canvas2
 * Because both paths are on same depth i.e. /canvases/canvas1 and /canvases/canvas2 have same parent /canvases, normal
 * breadcrumps aren't sufficient, and a custom implementation is required.
 */
export default function Breadcrumps() {
  const data = useLoaderData();

  return (
    <div className="breadcrumps">
      {data.name == "canvases" ? (
        <span>Canvases</span>
      ) : (
        <span>
          <Link to="/">Canvases</Link> / {data.name}
        </span>
      )}
    </div>
  );
}
