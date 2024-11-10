import { IConfigArrayItem } from "../types";
import { Accounts } from "./accounts";
import { Payments } from "./payments";
import { Todos } from "./todos";

// here should be the configuration for data, that we want to fetch

export enum Tabs {
  Todos = "Todos",
  Account = "Account",
  Payments = "Payments",
}

export const config: IConfigArrayItem<any, any, any>[] = [
  Todos,
  Accounts,
  Payments,
];

export type TabType = keyof typeof Tabs;
