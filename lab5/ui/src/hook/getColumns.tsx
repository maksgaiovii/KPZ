import { ColumnDef, createColumnHelper } from "@tanstack/react-table";
import { toUpperFirstSumbol } from "../util";
import { Cell } from "../global/components/Table/cell";

const columnHelper = createColumnHelper<any>();

const getKeys = (data: any) => {
  if(data===null || data===undefined){
    return [];
  }
  return Object.keys(data)
    .filter((item) => !["_v", "createdAt", "apdatedAt", "_id","contributorHistories","printingHouseBooks","textBooks"].includes(item))
    .filter((item) => typeof data[item] !== "function");
};

export const getColumns = (data: any): ColumnDef<any>[] => {
  return getKeys(data).map((item) => {
    if (typeof data[item] === "object") {
      return columnHelper.group({
        id: item,
        header: toUpperFirstSumbol([item])[0],
        columns: getColumns(data[item]),
      });
    }

    return {
      accessorKey: item,
      header: toUpperFirstSumbol([item])[0],
      cell: Cell,
      id: item,
    };
  });
};
