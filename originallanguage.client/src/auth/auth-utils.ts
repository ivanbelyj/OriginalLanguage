import axios from "axios";

export default class AuthUtils {
  public static getToken() {
    return localStorage.getItem("token"); // Todo: don't use local storage?
  }
  public static setToken(newToken: string) {
    localStorage.setItem("token", newToken);
    AuthUtils.updateAxiosDefaults();
  }
  public static removeToken() {
    localStorage.removeItem("token");
    AuthUtils.updateAxiosDefaults();
  }

  public static updateAxiosDefaults() {
    const token = AuthUtils.getToken();
    if (token)
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    else delete axios.defaults.headers.common["Authorization"];
  }
}
