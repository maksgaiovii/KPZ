import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the Book interface similar to the C# model
export interface IBook {
  bookId: number;
  bookTitle: string;
  numberOfPages: number;
  genre: string;
  languageCode: string;
  bookStatus: BookStatus;
}

export enum BookStatus {
  Pending = "Pending",
  Editing = "Editing",
  Illustrating = "Illustrating",
  CoverDesigning = "CoverDesigning",
  InProgress = "InProgress",
  Printing = "Printing",
  Completed = "Completed",
}

export const Books: IConfigArrayItem<IBook, IBook, Omit<IBook, "bookId">> = {
  tabName: "Books",
  api: new Api(`${baseUrl}Books`),
  tableConfig: {
    defaultColumns: ["bookTitle", "genre", "languageCode"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      console.log(data);
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ bookId }) => bookId.toString(),
  },
  formConfig: {
    fields: {
      bookTitle: {
        label: "Title",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      genre: {
        label: "Genre",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      languageCode: {
        label: "Language",
        as: "listbox",
        listboxProps: {
          options: [
            { value: "EN", label: "English" },
            { value: "ES", label: "Spanish" },
            { value: "FR", label: "French" },
            { value: "DE", label: "German" },
          ],
        },
      },
      bookStatus: {
        label: "Status",
        as: "listbox",
        listboxProps: {
          options: Object.values(BookStatus).map((status) => ({
            value: status,
            label: status,
          })),
        },
      },
    },
    yupSchema: yup.object().shape({
      bookTitle: yup.string().required("Title is required"),
      genre: yup.string().required("Genre is required"),
      languageCode: yup.string().required("Language is required"),
      bookStatus: yup
        .mixed<BookStatus>()
        .oneOf(Object.values(BookStatus), "Invalid status")
        .required("Status is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Book Form",
  },
};
