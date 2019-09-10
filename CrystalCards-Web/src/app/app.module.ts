import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { MaterialModule } from 'src/MaterialModule';
import { AppRoutingModule } from './app-routing.module';
import { CardListComponent } from './cards/card-list/card-list.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule } from '@angular/common/http';
import { CardTokenComponent } from './cards/card-token/card-token.component';
import { OpenCardComponent } from './cards/open-card/open-card.component';

@NgModule({
  declarations: [
    AppComponent,
    CardListComponent,
    CardTokenComponent,
    OpenCardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    FlexLayoutModule,
    HttpClientModule
   ],
   entryComponents: [
    OpenCardComponent
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
