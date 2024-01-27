import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthentificationHelper } from '../authentification/authentification-helper';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log('interceptor called');
        if (req.url.endsWith('/login')) {
            return next.handle(req);
        }
        const loginToken = AuthentificationHelper.getLoginToken();
        if (!loginToken.isExpired) {
            const authReq = req.clone({ headers: req.headers.set('Authorization', loginToken.id) });
            return next.handle(authReq);
        }
        console.log("Token expired, redirecting to login page");
        this.router.navigate(['']);
        AuthentificationHelper.clearLocalStorage();
        return new Observable();
    }
}