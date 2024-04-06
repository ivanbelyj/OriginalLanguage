import React from "react";
import { Tooltip } from "antd";
import SentenceUtils from "../../../common/utils/sentence-utils";
import { ITask, TaskType } from "../../models/models";

const SentenceWithHints: React.FC<{ task: ITask }> = ({ task }) => {
  const words = SentenceUtils.splitSeparatedSentence(task.question || "");
  const hints = SentenceUtils.splitSeparatedSentence(task.hint || "");
  const glosses = SentenceUtils.splitSeparatedSentence(task.glosses || "");

  const renderItemContent = (word: string, index: number) => {
    return (
      <>
        <span
          className={`sentence__word ${
            shouldShowTooltip() ? "sentence__word_with-hint" : ""
          }`}
        >
          {word}
        </span>
        {index < words.length - 1 && " "}
      </>
    );
  };

  const shouldShowTooltip = (): boolean => {
    return !!(
      task.taskType == TaskType.ElementsToTranslation &&
      (task.glosses || task.hint)
    );
  };

  const getTooltipTitle = (index: number) => {
    return (
      <>
        {hints[index]}
        {task.glosses && glosses[index] != hints[index] && (
          <>
            <hr />
            {glosses[index]}
          </>
        )}
      </>
    );
  };

  return (
    <div className="sentence">
      {words.map((word, index) =>
        shouldShowTooltip() ? (
          <Tooltip
            key={index}
            placement="bottom"
            title={getTooltipTitle(index)}
          >
            {renderItemContent(word, index)}
          </Tooltip>
        ) : (
          <span key={index}>{renderItemContent(word, index)}</span>
        )
      )}
    </div>
  );
};

export default SentenceWithHints;
