import axios, { AxiosRequestConfig } from "axios";

export default class AuthUtils {
  // #region Tokens storage
  public static getToken() {
    return localStorage.getItem("token");
  }
  private static setToken(newToken: string | null) {
    if (!newToken) AuthUtils.removeToken();
    else {
      localStorage.setItem("token", newToken);
      AuthUtils.updateAxiosDefaults();
    }
  }
  private static removeToken() {
    localStorage.removeItem("token");
    AuthUtils.updateAxiosDefaults();
  }

  private static getRefreshToken() {
    return localStorage.getItem("refreshToken");
  }
  private static setRefreshToken(newToken: string) {
    localStorage.setItem("refreshToken", newToken);
  }
  // #endregion

  public static async fetchTokens(username: string, password: string) {
    const response = await axios.post(
      import.meta.env.VITE_IDENTITY_URL + "connect/token",
      {
        client_id: "frontend",
        client_secret: "secret",
        grant_type: "password",
        username,
        password,
        scope: "offline_access content_write courses_learn",
      },
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      }
    );
    AuthUtils.setToken(response.data.access_token);
    AuthUtils.setRefreshToken(response.data.refresh_token);
    console.log("tokens fetched successfully", response);
    return response.data;
  }

  public static async refreshToken() {
    console.log("Refreshing token...");
    const refreshToken = AuthUtils.getRefreshToken();
    if (!refreshToken) {
      throw new Error("Refresh token not found");
    }

    try {
      const response = await axios.post(
        import.meta.env.VITE_IDENTITY_URL + "connect/token",
        {
          client_id: "frontend",
          client_secret: "secret",
          grant_type: "refresh_token",
          refresh_token: refreshToken,
          scope: "offline_access content_write courses_learn",
        },
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );

      if (response.status === 200) {
        console.log("Token refreshed successfully", response.data);
        AuthUtils.setToken(response.data.access_token);
        AuthUtils.setRefreshToken(response.data.refresh_token);
      } else {
        throw new Error("Failed to refresh token");
      }
    } catch (error) {
      console.error("Error refreshing token", error);
      throw error;
    }
  }

  public static async initAppAxios() {
    AuthUtils.updateAxiosDefaults();
    AuthUtils.useAxiosRefreshTokenInterceptor();
  }

  private static setAuthorizationHeader(request: any) {
    request.headers["Authorization"] = `Bearer ${AuthUtils.getToken()}`;
  }

  private static useAxiosRefreshTokenInterceptor() {
    interface IRetryQueueItem {
      resolve: (value?: any) => void;
      reject: (error?: any) => void;
      config: AxiosRequestConfig;
    }

    const refreshAndRetryQueue: IRetryQueueItem[] = [];

    // Flag to prevent multiple token refresh requests
    let isRefreshing = false;

    axios.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config;
        if (error.response.status === 401 && !originalRequest._retry) {
          if (!isRefreshing) {
            isRefreshing = true;
            originalRequest._retry = true;
            try {
              await AuthUtils.refreshToken();

              refreshAndRetryQueue.forEach(({ config, resolve, reject }) => {
                AuthUtils.setAuthorizationHeader(config);
                axios(config)
                  .then((response) => resolve(response))
                  .catch((err) => reject(err));
              });

              // Update the request headers with the new access token
              AuthUtils.setAuthorizationHeader(originalRequest);
              // Retry the original request
              return axios(originalRequest);
            } catch (refreshError) {
              console.error("Failed to refresh token", refreshError);
              return Promise.reject(error);
            } finally {
              isRefreshing = false;
            }
          }

          // Add the original request to the queue
          return new Promise<void>((resolve, reject) => {
            refreshAndRetryQueue.push({
              config: originalRequest,
              resolve,
              reject,
            });
          });
        }
        return Promise.reject(error);
      }
    );
  }

  private static updateAxiosDefaults() {
    const token = AuthUtils.getToken();
    if (token)
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    else delete axios.defaults.headers.common["Authorization"];
  }
}
