import { Api } from "../api";
import { IConfigArrayItem } from "../types";

export const baseUrl = "https://jsonplaceholder.typicode.com/";

export interface ITodos {
  id: number;
  title: string;
  completed: boolean;
}

export const Todos: IConfigArrayItem<ITodos, ITodos> = {
  tabName: "Todos",
  defaultColumns: [ "title", "completed"],
  api: new Api(`${baseUrl}todos`),
  mapToTable: (data = []) => data,
  mapBeforeUpdate: (data, columnName, newValue) => {
    return {
      ...data,
      [columnName]: newValue,
    };
  },
  getIdFromRow: ({ id }) => id.toString(),
};
