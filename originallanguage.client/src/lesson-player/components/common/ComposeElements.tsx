import React, { useState } from "react";
import {
  DragDropContext,
  DraggableLocation,
  DropResult,
} from "react-beautiful-dnd";
import ElementsArea, { getElementsAreaRowLength } from "./ElementsArea";

interface IElement {
  id: string;
  content: string;
  area: string;
}

const ComposeElements: React.FC = () => {
  const [elements, setElements] = useState<IElement[]>([
    ...Array.from({ length: 10 }, (_, index) => ({
      id: (index + 1).toString(),
      content: `Element (e) ${index + 1}`,
      area: "elements",
    })),
    ...Array.from({ length: 10 }, (_, index) => ({
      id: (index + 100).toString(),
      content: `Element (r) ${index + 1}`,
      area: "results",
    })),
  ]);

  const getAreaIdAndRowNumber = (str: string, location: DraggableLocation) => {
    const tmp = str.split("-");
    const areaId = tmp[0];
    const rowNumber = Number.parseInt(tmp[1]);

    const indexInArea = rowNumber * getElementsAreaRowLength() + location.index;

    let alienAreasCount = 0;
    let ourAreaCount = 0;
    for (const elem of elements) {
      if (elem.area === areaId) ourAreaCount++;
      else alienAreasCount++;

      if (ourAreaCount >= indexInArea) break;
    }

    const actualIndex: number = indexInArea + alienAreasCount;

    console.log(elements);
    console.log(areaId, rowNumber, actualIndex);

    return { areaId, rowNumber, actualIndex };
  };

  const handleDragEnd = (result: DropResult) => {
    const { destination, source } = result;

    // Droppable not found
    if (!destination) {
      return;
    }

    console.log("source");
    const { actualIndex: actualSourceIndex, areaId: sourceAreaId } =
      getAreaIdAndRowNumber(source.droppableId, source);

    console.log("destination");
    const { actualIndex: actualDestinationIndex, areaId: destinationAreaId } =
      getAreaIdAndRowNumber(destination.droppableId, destination);

    // Move from actual source index to actual destination index
    const reorderedElements: (IElement | null)[] = [...elements];
    const [removed] = reorderedElements.splice(actualSourceIndex, 1, null);

    if (removed && sourceAreaId != destinationAreaId) {
      removed.area = destinationAreaId;
    }

    reorderedElements.splice(actualDestinationIndex, 0, removed);

    const newElements: IElement[] = reorderedElements.filter(
      (x) => x !== null
    ) as IElement[];
    setElements(newElements);
  };

  return (
    <DragDropContext onDragEnd={handleDragEnd}>
      <ElementsArea
        droppableId="elements"
        items={elements.filter((x) => x.area === "elements")}
      />
      <ElementsArea
        droppableId="results"
        items={elements.filter((x) => x.area === "results")}
      />
    </DragDropContext>
  );
};

export default ComposeElements;
