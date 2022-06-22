// Proxy Address
let baseUrl = "";
if (import.meta.env.MODE === "production") {
  baseUrl = "http://localhost:5010/api/";
} else {
  baseUrl = "/api/";
}

export default {
  async get<T>(path: string): Promise<T | undefined> {
    const response = await fetch(`${baseUrl}${path}`, {
      method: "GET",
    });

    if (response.ok) {
      const result = await response.json();
      const data: T = result.results;
      return data;
    }
  },
  async post<T>(path: string, data: Object): Promise<T | undefined> {
    const response = await fetch(`${baseUrl}${path}`, {
      method: "POST",
      body: JSON.stringify(data),
      headers: [["content-type", "application/json"]],
    });

    if (response.status === 201) {
      const result = await response.json();
      const data: T = result;
      return data;
    } else {
      throw `Failed to post to ${path}`;
    }
  },
  async put<T>(path: string, data: Object): Promise<T | undefined> {
    const response = await fetch(`${baseUrl}${path}`, {
      method: "PUT",
      body: JSON.stringify(data),
      headers: [["content-type", "application/json"]],
    });

    if (response.ok) {
      const result = await response.json();
      const data: T = result;
      return data;
    } else {
      throw `Failed to put to ${path}`;
    }
  },
  async delete<T>(path: string): Promise<boolean> {
    const response = await fetch(`${baseUrl}${path}`, {
      method: "delete",
    });

    if (response.ok) {
      return true;
    } else {
      return false;
    }
  },
};
