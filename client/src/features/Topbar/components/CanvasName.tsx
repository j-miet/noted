import { useParams } from "react-router";
import { useEffect, useState } from "react";

import { canvasesApi } from "../../../api/canvasesApi";
import "./CanvasName.css";

export default function CanvasName() {
  const [name, SetName] = useState("");
  const urlParams = useParams();

  useEffect(() => {
    if (urlParams.canvasId) GetCanvasNameFromRoute();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const GetCanvasNameFromRoute = async (): Promise<void> => {
    try {
      const canvasId = Number(urlParams.canvasId);

      if (canvasId == null) return;
      const canvas = await canvasesApi.GetCanvasById(canvasId);

      SetName(canvas.name);
    } catch (error) {
      console.error(error);
    }
  };

  return <section>{name}</section>;
}
