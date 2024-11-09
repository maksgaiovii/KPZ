import { Api } from "../api";
import { IConfigArrayItem } from "../types";
import { set } from "../util";

export const baseUrl = "https://jsonplaceholder.typicode.com/";

export interface IUser {
  id: number;
  name: string;
  username: string;
  email: string;
  address: {
    street: string;
    suite: string;
    city: string;
    zipcode: string;
    geo: {
      lat: string;
      lng: string;
    };
  };
  phone: string;
  website: string;
  company: {
    name: string;
    catchPhrase: string;
    bs: string;
  };
}

export interface IUserRow {
  id: string;
  name: string;
  username: string;
  email: string;
  address: {
    street: string;
    suite: string;
  };
  phone: string;
  website: string;
  company: string;
}

export const Users: IConfigArrayItem<IUser, IUserRow, IUserRow> = {
  tabName: "Users",
  api: new Api(`${baseUrl}users`),
  tableConfig: {
    defaultColumns: [
      "name",
      "username",
      "email",
      {
        name: "address",
        arr: ["street", "suite"],
      },
      "phone",
      "website",
      "company",
    ],
    mapToTable: (data = []) =>
      data.map(
        ({ id, name, username, email, address, phone, website, company }) => ({
          id: id.toString(),
          name,
          username,
          email,
          address: {
            street: address.street,
            suite: address.suite,
          },
          phone,
          website,
          company: company.name,
        })
      ),
    mapBeforeUpdate: (data, columnName, newValue) => {
      console.log("🚀 ~ data:", data, columnName, newValue);
      if (columnName === "company") {
        return {
          ...data,
          company: {
            name: newValue,
          },
        } as unknown as Partial<IUser>;
      }

      const copy = JSON.parse(JSON.stringify(data));
      set(copy, columnName, newValue);
      return copy;
    },
    getIdFromRow: ({ id }) => id.toString(),
  },
  formConfig: {},
};
