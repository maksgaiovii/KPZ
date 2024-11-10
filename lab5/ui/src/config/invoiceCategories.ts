import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

export interface ICategory {
  id: number;
  name: string;
}

export const Categories: IConfigArrayItem<
  ICategory,
  ICategory,
  Omit<ICategory, "id">
> = {
  tabName: "Categories",
  api: new Api(`${baseUrl}InvoiceCategory`),
  tableConfig: {
    defaultColumns: ["name"],
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
      name: {
        label: "Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
    }),
    beforeSendToBekend: (data) => {
      return data;
    },
    formTitle: "Category Form",
  },
};
