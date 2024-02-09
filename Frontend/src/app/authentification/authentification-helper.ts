import { LoginToken } from "../login/models/login-token.model";

export class AuthentificationHelper {
  
  static isLoggedIn(): boolean {
    const loginTokenFromLS = sessionStorage.getItem("Authorization");
    return loginTokenFromLS !== null;
  }

  static setLoginToken(loginToken: LoginToken): void {
    if (loginToken == null)
      loginToken = new LoginToken();
      sessionStorage.setItem("Authorization", JSON.stringify(loginToken));
  }

  static getLoginToken(): LoginToken {
    let loginTokenFromLS = sessionStorage.getItem("Authorization");
    if (loginTokenFromLS == null)
      return new LoginToken();

    try {
      let loginToken: LoginToken = JSON.parse(loginTokenFromLS);
      return loginToken;
    }
    catch (e) {
      return new LoginToken();
    }
  }

  static clearLocalStorage() {
    sessionStorage.removeItem("Authorization");
  }
}