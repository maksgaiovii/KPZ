import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the ContributorHistory interface similar to the C# model
export interface IContributorHistory {
  id: number;
  bookId: number;
  contributorId: number;
  startDate: string; // Using ISO string format for Date
  finishDate?: string | null; // Nullable field for finish date
  contributorStatus: ContributorStatus;
}

export enum ContributorStatus {
  Active = "Active",
  Inactive = "Inactive",
  Completed = "Completed",
}

export const ContributorHistories: IConfigArrayItem<
  IContributorHistory,
  IContributorHistory,
  Omit<IContributorHistory, "id">
> = {
  tabName: "Contributor Histories",
  api: new Api(`${baseUrl}ContributorHistories`),
  tableConfig: {
    defaultColumns: ["startDate", "finishDate", "contributorStatus"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ id }) => id.toString(),
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
      contributorId: {
        label: "Contributor ID",
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
      contributorStatus: {
        label: "Contributor Status",
        as: "listbox",
        listboxProps: {
          options: Object.values(ContributorStatus).map((status) => ({
            value: status,
            label: status,
          })),
        },
      },
    },
    yupSchema: yup.object().shape({
      bookId: yup.number().required("Book ID is required"),
      contributorId: yup.number().required("Contributor ID is required"),
      startDate: yup.string().required("Start Date is required"),
      finishDate: yup.string().nullable(),
      contributorStatus: yup
        .mixed<ContributorStatus>()
        .oneOf(Object.values(ContributorStatus), "Invalid status")
        .required("Contributor Status is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Contributor History Form",
  },
};
