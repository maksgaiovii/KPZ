import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

export interface IAccount {
  id: number;
  name: string;
  currency: string;
}

export const Accounts: IConfigArrayItem<
  IAccount,
  IAccount,
  Omit<IAccount, "id">
> = {
  tabName: "Accounts",
  api: new Api(`${baseUrl}Account`),
  tableConfig: {
    defaultColumns: ["name", "currency", "id"],
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
      currency: {
        label: "Currency",
        as: "listbox",
        listboxProps: {
          options: [
            { value: "USD", label: "USD" },
            { value: "EUR", label: "EUR" },
            { value: "GBP", label: "GBP" },
            { value: "JPY", label: "JPY" },
          ],
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      currency: yup.string().required("Currency is required"),
    }),
    beforeSendToBekend: (data) => {
      console.log(data);
      return data;
    },
    formTitle: "Account Form",
  },
};
