import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { Invoices } from "./invoices";

export interface IPayment {
  id: number;
  date: string;
  amount: number;
  invoiceId: number;
}

export const Payments: IConfigArrayItem<
  IPayment,
  IPayment,
  Omit<IPayment, "id">
> = {
  tabName: "Payments",
  api: new Api(`${baseUrl}Payment`),
  tableConfig: {
    defaultColumns: ["id", "amount", "date", "invoiceId"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      if (columnName === "id") {
        throw new Error("Id is not updatable");
      }
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ id }) => id.toString(),
  },
  formConfig: {
    fields: {
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
          step: 0.01,
          min: 0,
        },
      },
      invoiceId: {
        label: "Invoice",
        as: "listbox",
        listboxProps: {},
        useGetOptions: () => {
          const { data } = useQuery({
            queryKey: [Invoices.tabName],
            queryFn: () => Invoices?.api?.getAll(),
            placeholderData: keepPreviousData,
          });
          return data
            ?.filter((item) => item.status === "unpaid")
            ?.map((item) => ({
              value: item.id as any,
              label: `${item.totalAmount} - ${item.dueDate.split("T")[0]}`,
            }));
        },
      },
    },
    yupSchema: yup.object().shape({
      amount: yup
        .number()
        .min(0, "Amount must be greater than or equal to 0")
        .required("Amount is required"),
      invoiceId: yup.number().required("Invoice is required"),
      date: yup.string().required("Date is required"),
    }),
    beforeSendToBekend: (data) => {
      return data;
    },
    formTitle: "Payment Form",
  },
};
