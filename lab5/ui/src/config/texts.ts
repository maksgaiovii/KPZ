import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the Text interface similar to the C# model
export interface IText {
  textId: number;
  authorName: string;
  authorSurname: string;
  receiptDate: string; // ISO string format for Date
  title: string;
}

export const Texts: IConfigArrayItem<IText, IText, Omit<IText, "textId">> = {
  tabName: "Texts",
  api: new Api(`${baseUrl}Texts`),
  tableConfig: {
    defaultColumns: ["authorName", "authorSurname", "receiptDate", "title"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ textId }) => textId.toString(),
  },
  formConfig: {
    fields: {
      authorName: {
        label: "Author Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      authorSurname: {
        label: "Author Surname",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      receiptDate: {
        label: "Receipt Date",
        as: "input",
        inputProps: {
          type: "date",
        },
      },
      title: {
        label: "Title",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
    },
    yupSchema: yup.object().shape({
      authorName: yup.string().required("Author Name is required"),
      authorSurname: yup.string().required("Author Surname is required"),
      receiptDate: yup.string().required("Receipt Date is required"),
      title: yup.string().required("Title is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Text Form",
  },
};
