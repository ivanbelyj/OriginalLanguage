import React, { useEffect } from "react";
import { ITaskProps } from "../../models/ITaskProps";
import { useElementsState } from "../../hooks/useElementsState";
import { ElementsArea } from "../common/ElementsArea";
import SentenceUtils from "../../../common/utils/sentence-utils";
import { SentenceElement } from "../common/SentenceElement";
import SentenceWithHints from "../common/SentenceWithHints";

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
      initialGiven: !!task.given ? splitSentence(task.given) : [],
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
      <SentenceWithHints task={task} />
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
