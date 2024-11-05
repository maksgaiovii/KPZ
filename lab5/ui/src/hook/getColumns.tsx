import { ColumnDef, createColumnHelper } from "@tanstack/react-table";
import { toUpperFirstSumbol } from "../util";
import { Cell } from "../global/components/Table/cell";
import { IColumnName } from "../types";

const columnHelper = createColumnHelper<any>();

export const getColumns = (
  names: (IColumnName | string)[],
  parentName = ''
): ColumnDef<any>[] => {
  return names.map((item,) => {
    if (typeof item === "object") {
      return columnHelper.group({
        id: parentName ? `${parentName}.${item.name}` : item.name,
        header: toUpperFirstSumbol([item.name])[0],
        columns: getColumns(item.arr, parentName ? `${parentName}.${item.name}` : item.name ),
      });
    }

    return {
      accessorKey: parentName ? `${parentName}.${item}` : item,
      header: toUpperFirstSumbol([item])[0],
      cell: Cell,
      id: parentName ? `${parentName}.${item}` : item,
    };
  });
};
