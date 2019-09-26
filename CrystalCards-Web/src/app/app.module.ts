import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { MaterialModule } from 'src/MaterialModule';
import { AppRoutingModule } from './app-routing.module';
import { CardListComponent } from './cards/card-list/card-list.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CardTokenComponent } from './cards/card-token/card-token.component';
import { OpenCardComponent } from './cards/open-card/open-card.component';
import { FormsModule } from '@angular/forms';
import { HttpErrorInterceptor } from './httpErrorIntercepter';
@NgModule({
  declarations: [
    AppComponent,
    CardListComponent,
    CardTokenComponent,
    OpenCardComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    FlexLayoutModule,
    HttpClientModule,
    FormsModule
   ],
   entryComponents: [
    OpenCardComponent
  ],

  providers: [
    {
    provide: HTTP_INTERCEPTORS,
     useClass: HttpErrorInterceptor,
     multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
