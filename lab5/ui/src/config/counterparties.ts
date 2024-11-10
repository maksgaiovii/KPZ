import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

export interface IConterpaty {
  id: string;
  name: string;
  address: string;
  email: string;
}

export const Conterpaties: IConfigArrayItem<IConterpaty, IConterpaty, IConterpaty> =
  {
    tabName: "Conterpaties",
    api: new Api(`${baseUrl}Counterparty`),
    tableConfig: {
      defaultColumns: ["name", "address", "email", "id"],
      mapToTable: (data = []) => data,
      mapBeforeUpdate: (data, columnName, newValue) => {
        return {
          ...data,
          [columnName]: newValue,
        };
      },
      getIdFromRow: ({ id }) => id.toString(),
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
          label: "Adress",
          as: "input",
          inputProps: {
            type: "text",
          },
        },
        email: {
          label: "Email",
          as: "input",
          inputProps: {
            type: "text",
          },
        },
        id: {
          label: "Tax",
          as: "input",
          inputProps: {
            type: "text",
          },
        },
      },
      yupSchema: yup.object().shape({
        name: yup.string().required("Name is required"),
        address: yup.string().required("Adress is required"),
        email: yup.string().required("Email is required"),
        id: yup.string().required("Tax is required"),
      }),
      beforeSendToBekend: (data) => {
        return data;
      },
      formTitle: "Conterpaty Form",
    },
  };
