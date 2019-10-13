import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CardListComponent} from '../app/cards/card-list/card-list.component'
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';


const routes: Routes = [
  { path: '', component:HomeComponent  },
  {path:'cards', component:CardListComponent, canActivate: [AuthGuard]}
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
