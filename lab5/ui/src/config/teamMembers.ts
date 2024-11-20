import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";

// Define the TeamMember interface similar to the C# model
export interface ITeamMember {
  teamMemberId: number;
  name: string;
  surname: string;
  email: string;
  role: "Editor" | "Illustrator" | "CoverDesigner";
}

export const TeamMembers: IConfigArrayItem<
  ITeamMember,
  ITeamMember,
  Omit<ITeamMember, "teamMemberId">
> = {
  tabName: "Team Members",
  api: new Api(`${baseUrl}TeamMembers`),
  tableConfig: {
    defaultColumns: ["name", "surname", "email", "role"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => {
      return {
        ...data,
        [columnName]: newValue,
      };
    },
    getIdFromRow: ({ teamMemberId }) => teamMemberId.toString(),
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
      surname: {
        label: "Surname",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      email: {
        label: "Email",
        as: "input",
        inputProps: {
          type: "email",
        },
      },
      role: {
        label: "Role",
        as: "listbox",
        listboxProps: {
          options: [
            { value: "Editor", label: "Editor" },
            { value: "Illustrator", label: "Illustrator" },
            { value: "CoverDesigner", label: "Cover Designer" },
          ],
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      surname: yup.string().required("Surname is required"),
      email: yup
        .string()
        .email("Email must be valid")
        .required("Email is required"),
      role: yup
        .string()
        .oneOf(["Editor", "Illustrator", "CoverDesigner"])
        .required("Role is required"),
    }),
    beforeSendToBackend: (data) => {
      return data;
    },
    formTitle: "Team Member Form",
  },
};
