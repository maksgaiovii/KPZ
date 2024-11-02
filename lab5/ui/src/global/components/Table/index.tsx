import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { useEffect, useMemo, useState } from "react";
import { Api } from "../../../api";
import { baseUrl } from "../../../config";
import { useTab } from "../../../context/tab";
import { getColumns } from "../../../hook/getColumns";
import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  Table as TanstackTable,
  useReactTable,
} from "@tanstack/react-table";
import { Pagination } from "./paggination";
import { useSkipper } from "../../../hook/useSkipper";

export const Table = () => {
  const { tab } = useTab();

  const api = useMemo(() => new Api(`${baseUrl}${tab}`), [tab]);

  const {
    data = [],
    isLoading,
    error,
  } = useQuery({
    queryKey: [tab],
    queryFn: () => api.getAll(),
    placeholderData: keepPreviousData,
    enabled: !!tab,
  });

  const [memoData, setMemoData] = useState(data);
  const [autoResetPageIndex, skipAutoResetPageIndex] = useSkipper();

  useEffect(() => {
    setMemoData(data);
  }, [data]);

  const columns = useMemo(
    () => getColumns(memoData?.[0] || { id: "" }),
    [memoData]
  );

  const table = useReactTable({
    columns,
    data: memoData,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    autoResetPageIndex,
    meta: {
      updateData: (rowIndex, columnId, value) => {
        const previousData = memoData[rowIndex][columnId];

        setMemoData((prev) =>
          prev.map((item, index) =>
            index === rowIndex ? { ...item, [columnId]: value } : item
          )
        );

        const id = table.getRowModel().rows[rowIndex].original.id;

        api
          .put(id, {
            [columnId]: value,
          })
          .catch(() => {
            setMemoData((prev) =>
              prev.map((item, index) =>
                index === rowIndex
                  ? { ...item, [columnId]: previousData }
                  : item
              )
            );
            console.error("Не вдалося оновити дані на сервері.");
          });

        skipAutoResetPageIndex();
      },
    },
  });

  if (isLoading) return <div>Loading...</div>;

  if (error) return <div>Error: {error.message}</div>;

  return (
    <>
      <div className="relative overflow-x-auto shadow-md sm:rounded-lg no-scrollbar my-2">
        <table
          key={tab}
          className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
        >
          {GetHeader(table)}
          <tbody>
            {table.getRowModel().rows.map((row) => {
              return (
                <tr
                  key={row.id}
                  className="bg-white border-b dark:bg-gray-800 dark:border-gray-700"
                >
                  {row.getVisibleCells().map((cell) => {
                    return (
                      <td
                        key={cell.id}
                        className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                      >
                        {flexRender(
                          cell.column.columnDef.cell,
                          cell.getContext()
                        )}
                      </td>
                    );
                  })}
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>

      <Pagination table={table} />
    </>
  );
};

function GetHeader(table: TanstackTable<any>) {
  const header = (
    <thead className="text-xs text-gray-700 bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
      {table.getHeaderGroups().map((headerGroup) => (
        <tr key={headerGroup.id}>
          {headerGroup.headers.map((header) => {
            return (
              <th
                key={header.id}
                colSpan={header.colSpan}
                scope="col"
                className="px-6 py-3"
              >
                {header.isPlaceholder ? null : (
                  <>
                    <div
                      {...{
                        className: header.column.getCanSort()
                          ? "cursor-pointer select-none"
                          : "",
                        onClick: header.column.getToggleSortingHandler(),
                      }}
                    >
                      {flexRender(
                        header.column.columnDef.header,
                        header.getContext()
                      )}
                      {{
                        asc: " 🔼",
                        desc: " 🔽",
                        false: " 🔃",
                      }[header.column.getIsSorted() as string] ?? null}
                    </div>
                  </>
                )}
              </th>
            );
          })}
        </tr>
      ))}
    </thead>
  );

  return header;
}
