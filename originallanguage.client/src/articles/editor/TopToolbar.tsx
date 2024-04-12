import React, { FC, forwardRef, useEffect, useRef } from "react";
import {
  BasicFormattingButtonGroup,
  CreateTableButton,
  DataTransferButtonGroup,
  HeadingLevelButtonGroup,
  HistoryButtonGroup,
  ListButtonGroup,
  Toolbar,
  VerticalDivider,
} from "@remirror/react";

interface ITopToolbarProps {}

export const TopToolbar = (props: ITopToolbarProps) => {
  return (
    <Toolbar
      id="edit-article-toolbar"
      style={{
        display: "flex",
        flexWrap: "wrap",
        rowGap: "8px",
        padding: "4px 21px",

        position: "fixed",
        left: 80,
        right: 0,
        top: 45,
        zIndex: 1,
      }}
    >
      <HistoryButtonGroup />
      <VerticalDivider />
      <DataTransferButtonGroup />
      <VerticalDivider />
      <HeadingLevelButtonGroup />
      <VerticalDivider />
      <BasicFormattingButtonGroup />
      <VerticalDivider />
      <ListButtonGroup>
        <CreateTableButton />
      </ListButtonGroup>
    </Toolbar>
  );
};
