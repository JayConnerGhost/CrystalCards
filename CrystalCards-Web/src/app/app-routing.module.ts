import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CardListComponent} from '../app/cards/card-list/card-list.component'
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component:HomeComponent  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
