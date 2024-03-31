import React, { useEffect } from "react";
import { ITaskProps } from "../../models/ITaskProps";
import { useElementsState } from "../../hooks/useElementsState";
import { ElementsArea } from "../common/ElementsArea";

const ComposeElementsTask: React.FC<ITaskProps> = ({
  task,
  currentAnswer,
  setCurrentAnswer,
}) => {
  const {
    givenElements,
    resultElements,
    moveGivenElement,
    moveResultElement,
    reset,
  } = useElementsState(getInitialElements());

  useEffect(() => {
    setCurrentAnswer(elementsToSentence(resultElements));
  }, [resultElements, setCurrentAnswer]);

  useEffect(() => {
    reset(getInitialElements());
  }, [task]);

  function getInitialElements() {
    return {
      initialGiven: splitSentence(task.question),
      initialResult: [],
    };
  }

  function splitSentence(sentence: string) {
    return sentence.split(" ");
  }

  function elementsToSentence(elements: string[]) {
    return elements.join(" ");
  }

  return (
    <div className="compose-elements">
      <div>
        <ElementsArea
          items={resultElements}
          onElementClick={moveResultElement}
        />
      </div>

      <div>
        <ElementsArea items={givenElements} onElementClick={moveGivenElement} />
      </div>
    </div>
  );
};

export default ComposeElementsTask;
