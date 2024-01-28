import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';

const importsAndExportsInternal: NgModule['exports'] = [
    CommonModule,
    FormsModule,
    RouterModule
];

@NgModule({
    imports: importsAndExportsInternal,
    exports: importsAndExportsInternal
})
export class SharedModule { }
