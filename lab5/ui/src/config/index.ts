import { IConfigArrayItem } from "../types";
import { Accounts } from "./accounts";
import { Conterpaties } from "./counterparties";
import { Categories } from "./invoiceCategories";
import { Invoices } from "./invoices";
import { Payments } from "./payments";
import { Todos } from "./todos";

// here should be the configuration for data, that we want to fetch

export enum Tabs {
  Todos = "Todos",
  Account = "Account",
  Payments = "Payments",
  Invoices = "Invoices",
  Categories = "Categories",
  Conterpaties = "Conterpaties",
}

export const config: IConfigArrayItem<any, any, any>[] = [
  Todos,
  Accounts,
  Payments,
  Invoices,
  Categories,
  Conterpaties,
];

export type TabType = keyof typeof Tabs;
