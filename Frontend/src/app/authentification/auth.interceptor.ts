import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthentificationHelper } from '../authentification/authentification-helper';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.url.endsWith('/login')) {
            return next.handle(req);
        }
        if (!AuthentificationHelper.getLoginToken().isExpired) {
            return next.handle(req);
        } else {
            console.log("Token expired, redirecting to login page");
            this.router.navigate(['']);
            AuthentificationHelper.clearLocalStorage();
            return new Observable();
        }
    }
}
