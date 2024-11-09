import { Api } from "../api";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

export const baseUrl = "https://jsonplaceholder.typicode.com/";

export interface ITodos {
  id: number;
  title: string;
  completed: boolean;
}

export const Todos: IConfigArrayItem<ITodos, ITodos, Omit<ITodos, "id">> = {
  tabName: "Todos",
  api: new Api(`${baseUrl}todos`),
  tableConfig: {
    defaultColumns: ["completed", "title", "id"],
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
      title: {
        label: "Title",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      completed: {
        label: "Completed",
        as: "listbox",
        listboxProps: {
          options: [
            { value: "true", label: "True" },
            { value: "false", label: "False" },
          ],
        },
      },
    },
    yupSchema: yup.object().shape({
      title: yup.string().required("Title is required"),
      completed: yup.boolean().required("Completed is required"),
    }),
    beforeSendToBekend: (data) => {
      console.log(data);
      return {
        id: Math.floor(Math.random() * 1000),
        ...data,
      } as ITodos;
    },
    formTitle: "Todo Form",
  },
};
