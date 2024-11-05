import { Api } from "../api";

export type IColumnName = {
  name: string;
  arr: (string | IColumnName)[];
};

export type IConfigArrayItem<Data = any, Row = any> = {
  tabName: string;
  defaultColumns: (IColumnName | string)[];
  mapToTable: (data?: Data[]) => Row[];
  mapBeforeUpdate: (
    data: Row,
    columnName: string,
    newValue: string
  ) => Partial<Data>;
  getIdFromRow: (row: Row) => string;
  api: Api<Data>;
};
