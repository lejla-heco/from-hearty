import { ApplicationConfig } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

export function useHashLocationStrategy(config: ApplicationConfig): void {
    config.providers.push({ provide: LocationStrategy, useClass: HashLocationStrategy })

    const includesHash = window.location.href.includes("#");
    if(!includesHash)
        document.getElementsByTagName("base")[0].href = "/#/";
}