import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the PrintingHouseBook interface similar to the C# model
export interface IPrintingHouseBook {
  bookId: number;
  printingHouseId: number;
  startDate: string; // ISO string format for Date
  finishDate?: string | null; // Nullable finish date
  booksQuantity: number;
}

export const PrintingHouseBooks: IConfigArrayItem<
  IPrintingHouseBook,
  IPrintingHouseBook,
  IPrintingHouseBook
> = {
  tabName: "Printing House Books",
  api: new Api(`${baseUrl}PrintingHouseBooks`),
  tableConfig: {
    defaultColumns: ["startDate", "finishDate", "booksQuantity"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ bookId, printingHouseId }) =>
      `${bookId}-${printingHouseId}`, // Composite key (bookId, printingHouseId)
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
      printingHouseId: {
        label: "Printing House ID",
        as: "input",
        inputProps: {
          type: "number",
        },
      },
      startDate: {
        label: "Start Date",
        as: "input",
        inputProps: {
          type: "date",
        },
      },
      finishDate: {
        label: "Finish Date",
        as: "input",
        inputProps: {
          type: "date",
        },
      },
      booksQuantity: {
        label: "Books Quantity",
        as: "input",
        inputProps: {
          type: "number",
        },
      },
    },
    yupSchema: yup.object().shape({
      bookId: yup.number().required("Book ID is required"),
      printingHouseId: yup.number().required("Printing House ID is required"),
      startDate: yup.string().required("Start Date is required"),
      finishDate: yup.string().nullable(),
      booksQuantity: yup
        .number()
        .required("Books Quantity is required")
        .min(1, "Quantity must be at least 1"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Printing House Book Form",
  },
};
