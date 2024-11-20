import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the TextBook interface similar to the C# model
export interface ITextBook {
  bookId: number;
  textId: number;
}

export const TextBooks: IConfigArrayItem<
  ITextBook,
  ITextBook,
  Omit<ITextBook, "bookId" | "textId">
> = {
  tabName: "Text Books",
  api: new Api(`${baseUrl}TextBooks`),
  tableConfig: {
    defaultColumns: ["bookId", "textId"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ bookId, textId }) => `${bookId}-${textId}`,
  },
  formConfig: {
    fields: {
      bookId: {
        label: "Book ID",
        as: "input",
        inputProps: {
          type: "number",
        },
      },
      textId: {
        label: "Text ID",
        as: "input",
        inputProps: {
          type: "number",
        },
      },
    },
    yupSchema: yup.object().shape({
      bookId: yup.number().required("Book ID is required"),
      textId: yup.number().required("Text ID is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "TextBook Form",
  },
};
