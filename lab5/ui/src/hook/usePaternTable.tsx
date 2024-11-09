import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { config } from "../config";
import { useTab } from "../context/tab";
import { useCallback, useEffect, useMemo, useState } from "react";
import { useSkipper } from "./useSkipper";
import { getColumns } from "./getColumns";
import {
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { get, set } from "../util";

export const usePaternTable = () => {
  const { tab, isModalOpen, setModalOpen } = useTab();
  const [deleteId, setDeleteId] = useState<string | null | undefined>(null);

  const selectedTab = useMemo(
    () => config.find(({ tabName }) => tab === tabName),
    [tab]
  );

  const { data, isLoading, error, refetch } = useQuery({
    queryKey: [tab],
    queryFn: () => selectedTab?.api?.getAll(),
    placeholderData: keepPreviousData,
    enabled: !!selectedTab,
  });

  const [memoData, setMemoData] = useState(data);
  const [autoResetPageIndex, skipAutoResetPageIndex] = useSkipper();

  useEffect(() => {
    setMemoData(selectedTab?.tableConfig?.mapToTable(data) || []);
  }, [data, selectedTab]);

  const columns = useMemo(
    () => getColumns(selectedTab?.tableConfig?.defaultColumns || []),
    [selectedTab?.tableConfig?.defaultColumns]
  );

  const table = useReactTable({
    columns: columns || [],
    data: memoData,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    autoResetPageIndex,
    enableMultiSort: true,
    meta: {
      updateData: (rowIndex, columnId, value) => {
        const previousData = get(memoData[rowIndex], columnId);
        const body = selectedTab?.tableConfig?.mapBeforeUpdate(
          memoData[rowIndex],
          columnId,
          value as string
        );
        setMemoData((prev) => {
          set(prev[rowIndex], columnId, value);
          return prev;
        });

        const id = selectedTab?.tableConfig?.getIdFromRow(
          table.getRowModel().rows[rowIndex].original
        );

        selectedTab?.api?.put(id, body as any).catch(() => {
          setMemoData((prev) => {
            set(prev[rowIndex], columnId, previousData);
            return prev;
          });
          console.error("Не вдалося оновити дані на сервері.");
        });

        skipAutoResetPageIndex();
      },
      deleteData: (rowIndex) => {
        const id = selectedTab?.tableConfig?.getIdFromRow(
          table.getRowModel().rows[rowIndex].original
        );

        setDeleteId(id);
      },
    },
  });

  const onSubmit = useCallback(
    async (data) => selectedTab?.api.post(data),
    [selectedTab]
  );
  const onDelete = useCallback(
    async () => selectedTab?.api.delete(deleteId as string),
    [selectedTab, deleteId]
  );

  const closeModal = useCallback(() => setModalOpen(false), [setModalOpen]);
  const closeModalDeleteModal = useCallback(() => setDeleteId(null), []);
  return {
    table,
    isLoading,
    error,
    refetch,
    isModalOpen,
    closeModal,
    onSubmit,
    selectedTab,
    tab,
    isDeleteModalOpen: !!deleteId,
    closeModalDeleteModal,
    onDelete,
  };
};
