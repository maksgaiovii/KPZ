/* eslint-disable @typescript-eslint/no-explicit-any */
import { IConfigArrayItem } from "../types";
import { Books } from "./books";
import { ContributorHistories } from "./contibutorHistory";
import { PrintingHouses } from "./printingHouse";
import { PrintingHouseBooks } from "./printingHouseBook";
import { TeamMembers } from "./teamMembers";
import { TextBooks } from "./textBook";
import { Texts } from "./texts";

// Enum for Tabs
export enum Tabs {
  Books = "Books",
  ContributorHistories = "ContributorHistories",
  PrintingHouses = "PrintingHouses",
  PrintingHouseBooks = "PrintingHouseBooks",
  TeamMembers = "TeamMembers",
  TextBooks = "TextBooks",
  Texts = "Texts",
}

// Configuration for each tab with their respective types based on IConfigArrayItem
export const config: IConfigArrayItem<any, any, any>[] = [
  Books,
  PrintingHouses,
  TeamMembers,
  Texts,
];

// Type for TabType using the Tabs enum
export type TabType = keyof typeof Tabs;
