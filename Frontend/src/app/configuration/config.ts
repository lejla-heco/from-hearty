import { environment } from "../../environments/environment";
import { AuthentificationHelper } from "../authentification/authentification-helper";
import { LoginToken } from "../login/models/login-token.model";

export class Config {
    static serverAddress = environment.BACKEND_URL;

    static httpOptions = function () {
        let authentificationToken: LoginToken = AuthentificationHelper.getLoginToken();
        let token = "";

        if (authentificationToken != null)
            token = authentificationToken.id;

        return { headers: { 'Authorization': token, } };
    }

}