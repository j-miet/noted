const BACKEND_URL = "https://localhost:5000";

/**
 * Send requests to backend server and receive a response object
 */
export const apiRequest = async (endpoint: string, options = {}) => {
  const response = await fetch(BACKEND_URL + endpoint, {
    headers: {
      "Content-Type": "application/json",
    },
    ...options,
  });

  const data = await response.json().catch(() => null); // no idea how to set type for this instead of using 'any'

  if (!response.ok) {
    throw new Error(data.error || "Something went wrong");
  }

  return data;
};
