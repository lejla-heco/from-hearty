/// <reference types="@angular/localize" />

import { bootstrapApplication } from '@angular/platform-browser';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { routes } from './routes';
import { useHashLocationStrategy } from './.shared/use-hash-location-strategy.helper';
import { provideRouter } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './app/authentification/auth.interceptor';

const config: ApplicationConfig = { providers: [provideAnimations()] };

useHashLocationStrategy(config);
config.providers.push(provideRouter(routes));
config.providers.push(importProvidersFrom(
    ToastrModule.forRoot(),
));

config.providers.push(provideAnimations());
config.providers.push(provideHttpClient(
    withInterceptorsFromDi(),
));
config.providers.push({
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
});

bootstrapApplication(AppComponent, config);
