import {NgModule} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatToolbarModule} from '@angular/material';

@NgModule({
  imports: [
      MatButtonModule,
       MatCheckboxModule,
       MatToolbarModule
    ],
  exports: [
      MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule
],
})
export class MaterialModule { }