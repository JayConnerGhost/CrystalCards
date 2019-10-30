import {NgModule} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatToolbarModule} from '@angular/material';
import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTabsModule} from '@angular/material/tabs';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material';
import {MatListModule} from '@angular/material';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonToggleModule} from '@angular/material';
import {OpenCardComponent } from './app/cards/open-card/open-card.component';
import {MatTableModule} from '@angular/material/table';


@NgModule({
  imports: [
      MatButtonModule,
       MatCheckboxModule,
       MatToolbarModule,
       MatCardModule,
       MatDialogModule,
       MatTabsModule,
       MatGridListModule,
      MatFormFieldModule,
      MatInputModule,
      MatListModule,
      MatIconModule,
      MatButtonToggleModule,
      MatTableModule

    ],
  exports: [
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatCardModule,
    MatDialogModule,
    MatTabsModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule,
    MatIconModule,
    MatButtonToggleModule,
    MatTableModule

],

})
export class MaterialModule { }
