import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { Accounts } from "./accounts";
import { Categories } from "./invoiceCategories";

export interface IInvoice {
  id: number;
  accountId: number;
  counterpartyId: string;
  invoiceDate: string;
  dueDate: string;
  totalAmount: number;
  status: string;
  categoryId: number;
}

export const Invoices: IConfigArrayItem<
  IInvoice,
  IInvoice,
  Omit<IInvoice, "id">
> = {
  tabName: "Invoices",
  api: new Api(`${baseUrl}Invoice`),
  tableConfig: {
    defaultColumns: ["totalAmount", "invoiceDate", "status", "dueDate", "id"],
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
      dueDate: {
        label: "Date",
        as: "input",
        inputProps: {
          type: "date",
        },
      },
      totalAmount: {
        label: "Amount",
        as: "input",
        inputProps: {
          type: "number",
          min: 0,
        },
      },
      accountId: {
        label: "Account",
        as: "listbox",
        listboxProps: {},
        useGetOptions: () => {
          const { data } = useQuery({
            queryKey: [Accounts.tabName],
            queryFn: () => Accounts?.api?.getAll(),
            placeholderData: keepPreviousData,
          });
          return data?.map((account) => ({
            value: account.id as any,
            label: account.name,
          }));
        },
      },
      categoryId: {
        label: "Category",
        as: "listbox",
        listboxProps: {},
        useGetOptions: () => {
          const { data } = useQuery({
            queryKey: [Categories.tabName],
            queryFn: () => Categories?.api?.getAll(),
            placeholderData: keepPreviousData,
          });
          return data?.map((category) => ({
            value: category.id as any,
            label: category.name,
          }));
        },
      },
      counterpartyId: {
        label: "Counterparty Tax",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
    },
    yupSchema: yup.object().shape({
      totalAmount: yup
        .number()
        .min(0, "Amount must be greater than or equal to 0")
        .required("Amount is required"),
      dueDate: yup
        .string()
        .matches(
          /^(\d{4})-(\d{2})-(\d{2})$/,
          "Date must be in the format YYYY-MM-DD"
        ),
      accountId: yup.number().required("Account is required"),
      categoryId: yup.number().required("Category is required"),
      counterpartyId: yup.string().required("Counterparty is required"),
    }),
    beforeSendToBekend: (data) => {
      data.invoiceDate = new Date().toISOString();
      data.status = "unpaid";
      return data;
    },
    formTitle: "Invoice Form",
  },
};
