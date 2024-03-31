import { useState } from "react";

export interface IInitialElementsProps {
  initialResult: string[];
  initialGiven: string[];
}

export const useElementsState = ({
  initialGiven,
  initialResult,
}: IInitialElementsProps) => {
  const [resultElements, setResultElements] = useState<string[]>(initialResult);
  const [givenElements, setGivenElements] = useState<string[]>(initialGiven);

  const reset = ({ initialGiven, initialResult }: IInitialElementsProps) => {
    setResultElements(initialResult);
    setGivenElements(initialGiven);
  };

  const moveResultElement = (localId: number) => {
    const elementToAppend = resultElements.at(localId)!;
    setResultElements((prev) => prev.filter((_, index) => index !== localId));
    setGivenElements((prev) => [...prev, elementToAppend]);
  };
  const moveGivenElement = (localId: number) => {
    const elementToAppend = givenElements.at(localId)!;
    setGivenElements((prev) => prev.filter((_, index) => index !== localId));
    setResultElements((prev) => [...prev, elementToAppend]);
  };

  return {
    resultElements,
    givenElements,
    moveGivenElement,
    moveResultElement,
    reset,
  };
};
