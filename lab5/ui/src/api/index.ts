export interface IApi<TData> {
  getAll: () => Promise<TData>;
  getById: (id: string) => Promise<TData>;
  post: (data: TData) => Promise<TData>;
  put: (id: string, data: Partial<TData>) => Promise<TData>;
  delete: (id: string) => Promise<TData>;
  patch: (id: string, data: Partial<TData>) => Promise<TData>;
}

export class Api<TData> implements IApi<TData> {
  constructor(private baseUrl: string) {}

  async getAll() {
    const response = await fetch(this.baseUrl);
    return response.json();
  }

  async getById(id: string) {
    const response = await fetch(`${this.baseUrl}/${id}`);
    return response.json();
  }

  async post(data: TData) {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return response.json();
  }

  async put(id: string, data: Partial<TData>) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return response.json();
  }

  async delete(id: string) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "DELETE",
    });
    return response.json();
  }

  async patch(id: string, data: Partial<TData>) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return response.json();
  }
}