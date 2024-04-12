import { PropsWithChildren, useCallback, useEffect, useState } from "react";
import { PlaceholderExtension, wysiwygPreset } from "remirror/extensions";
import { TableExtension } from "@remirror/extension-react-tables";
import {
  EditorComponent,
  FloatingToolbar,
  OnChangeJSON,
  Remirror,
  TableComponents,
  ThemeProvider,
  useRemirror,
  useRemirrorContext,
} from "@remirror/react";
import { AllStyledComponent } from "@remirror/styles/emotion";

import { TopToolbar } from "./TopToolbar";

import type { CreateEditorStateProps } from "remirror";
import type { RemirrorProps, UseThemeProps } from "@remirror/react";
import type { RemirrorJSON } from "@remirror/core";

export interface ReactEditorProps
  extends Pick<CreateEditorStateProps, "stringHandler">,
    Pick<RemirrorProps, "initialContent" | "editable" | "autoFocus" | "hooks"> {
  placeholder?: string;
  theme?: UseThemeProps["theme"];
  onChange: (json: RemirrorJSON) => void;
}

export interface ArticleEditorProps extends Partial<ReactEditorProps> {}

export const ArticleEditor = ({
  placeholder,
  stringHandler,
  children,
  theme,
  onChange,
  ...rest
}: PropsWithChildren<ArticleEditorProps>) => {
  const extensions = useCallback(
    () => [
      new PlaceholderExtension({ placeholder }),
      new TableExtension(),
      ...wysiwygPreset(),
    ],
    [placeholder]
  );
  const { manager } = useRemirror({ extensions, stringHandler });

  return (
    <AllStyledComponent>
      <ThemeProvider theme={theme}>
        <Remirror manager={manager} {...rest}>
          <TopToolbar />

          <EditorComponent />
          <FloatingToolbar />
          <TableComponents />
          {children}
          {onChange && <OnChangeJSON onChange={onChange} />}
        </Remirror>
      </ThemeProvider>
    </AllStyledComponent>
  );
};
