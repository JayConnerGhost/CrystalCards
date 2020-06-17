import { NgModule } from '@angular/core';
import {CdkTableModule} from '@angular/cdk/table';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
const modules = [
  MatCardModule,
  MatInputModule,
  MatFormFieldModule,
  ReactiveFormsModule
];

@NgModule({
  imports: modules,
  exports: modules,
})
export class MaterialModule {
}
