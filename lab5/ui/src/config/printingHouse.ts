import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the PrintingHouse interface similar to the C# model
export interface IPrintingHouse {
  printingHouseId: number;
  name: string;
  address: string;
}

export const PrintingHouses: IConfigArrayItem<
  IPrintingHouse,
  IPrintingHouse,
  Omit<IPrintingHouse, "printingHouseId">
> = {
  tabName: "Printing Houses",
  api: new Api(`${baseUrl}PrintingHouse`),
  tableConfig: {
    defaultColumns: ["name", "address"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ printingHouseId }) => printingHouseId.toString(),
  },
  formConfig: {
    fields: {
      name: {
        label: "Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      address: {
        label: "Address",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      address: yup.string().required("Address is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Printing House Form",
  },
};
