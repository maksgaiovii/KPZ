import { IConfigArrayItem } from "../types";
import { Todos } from "./todos";
import { Users } from "./users";

// here should be the configuration for data, that we want to fetch
export const baseUrl = "http://localhost:5064/api/";

export enum Tabs {
  Todos = "Todos",
  Users = "Users",
}

export const config: IConfigArrayItem<any, any, any>[] = [Todos, Users];

export type TabType = keyof typeof Tabs;
