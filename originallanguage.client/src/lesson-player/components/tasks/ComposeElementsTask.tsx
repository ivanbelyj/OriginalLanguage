import React from "react";
import { ITaskProps } from "../../models/ITaskProps";
import ComposeElements from "../common/ComposeElements";

const ComposeElementsTask: React.FC<ITaskProps> = ({}) => {
  return (
    <>
      <div>Todo: elementsToText</div>
      <ComposeElements />
    </>
  );
};

export default ComposeElementsTask;
