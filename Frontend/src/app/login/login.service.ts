import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    api = {
        login: '/login',
    }
}