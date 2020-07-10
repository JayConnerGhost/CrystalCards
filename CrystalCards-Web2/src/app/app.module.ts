import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppComponent } from './app.component';
import { CardListComponent } from './card-list/card-list.component';
import { ProjectComponent } from './project/project.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './app.meterial.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import {AuthInterceptor} from './AuthInterceptor';
import {ToolbarComponent} from './toolbar/toolbar.component';
import { AddCardComponent } from './add-card/add-card.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    CardListComponent,
    ProjectComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ToolbarComponent,
    AddCardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FlexLayoutModule,
    MatDialogModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  entryComponents: [
    AddCardComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
