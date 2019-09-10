import {NgModule} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatToolbarModule} from '@angular/material';
import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTabsModule} from '@angular/material/tabs';

import {OpenCardComponent } from './app/cards/open-card/open-card.component';
@NgModule({
  imports: [
      MatButtonModule,
       MatCheckboxModule,
       MatToolbarModule,
       MatCardModule,
       MatDialogModule,
       MatTabsModule

    ],
  exports: [
      MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatCardModule,
    MatDialogModule,
    MatTabsModule

],

})
export class MaterialModule { }
