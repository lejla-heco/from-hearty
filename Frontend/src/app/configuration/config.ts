import { AuthentificationHelper } from "../authentification/authentification-helper";
import { LoginToken } from "../login/models/login-token.model";

export class Config {
    static serverAddress = "http://localhost:5000";

    static httpOptions = function () {
        let authentificationToken: LoginToken = AuthentificationHelper.getLoginToken();
        let token = "";

        if (authentificationToken != null)
            token = authentificationToken.id;

        return { headers: { 'Authorization': token, } };
    }

}