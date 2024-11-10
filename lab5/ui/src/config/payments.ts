import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

export interface IPayment {
  id: number;
  name: string;
  date: string;
  amount: number;
}

export const Payments: IConfigArrayItem<
  IPayment,
  IPayment,
  Omit<IPayment, "id">
> = {
  tabName: "Payments",
  api: new Api(`${baseUrl}Payment`),
  tableConfig: {
    defaultColumns: ["name", "amount", "date", "id"],
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
      date: {
        label: "Date",
        as: "input",
        inputProps: {
          type: "date",
          min: Date.now(),
        },
      },
      amount: {
        label: "Amount",
        as: "input",
        inputProps: {
          type: "number",
          min: 0,
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      date: yup
        .string()
        .matches(
          /^(\d{4})-(\d{2})-(\d{2})$/,
          "Date must be in the format YYYY-MM-DD"
        ),
      amount: yup
        .number()
        .min(0, "Amount must be greater than or equal to 0")
        .required("Amount is required"),
    }),
    beforeSendToBekend: (data) => {
      console.log(data);
      return (data.date = new Date(data.date!).toISOString());
    },
    formTitle: "Payment Form",
  },
};
