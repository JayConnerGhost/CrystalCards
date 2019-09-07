import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { MaterialModule } from 'src/MaterialModule';
import { AppRoutingModule } from './app-routing.module';
import { CardListComponent } from './cards/card-list/card-list.component';

@NgModule({
  declarations: [
    AppComponent,
    CardListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
   ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
