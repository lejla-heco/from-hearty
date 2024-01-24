import { bootstrapApplication } from '@angular/platform-browser';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { routes } from './routes';
import { provideRouter } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';

const config: ApplicationConfig = { providers: [] };

config.providers.push(provideRouter(routes));
config.providers.push(importProvidersFrom(
    ToastrModule.forRoot(),
    
));
config.providers.push(provideAnimations());
config.providers.push(provideHttpClient());

bootstrapApplication(AppComponent, config);
