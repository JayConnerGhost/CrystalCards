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
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AddCardComponent } from './cards/add-card/add-card.component';
import { ToastrModule } from 'ngx-toastr';
import { OpenForAddCardComponent } from './cards/open-for-add-card/open-for-add-card.component';
import { MoodWallComponent } from './cards/mood-wall/mood-wall.component';
import { OverlayModule } from '@angular/cdk/overlay';
import { FilePreviewOverlayComponent } from './file-preview-overlay.component';
import { FilePreviewOverlayService } from './services/file-preview-overlay.service';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthInterceptor } from './AuthIntercepter';
import { CardPrintComponent } from './cards/card-print/card-print.component';
import {NgxPrintModule} from 'ngx-print';
import { AdminMenuComponent } from './users/admin-menu/admin-menu.component';
import { OpenUserManagementComponent } from './users/open-user-management/open-user-management.component';
import {GeneralMenuComponent} from "./users/general-menu/general-menu.component";
import { AddProjectComponent } from './projects/add-project/add-project.component';
import { ListProjectComponent } from './projects/list-project/list-project.component';



export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    CardListComponent,
    CardTokenComponent,
    OpenCardComponent,
    AddCardComponent,
    OpenForAddCardComponent,
    MoodWallComponent,
    FilePreviewOverlayComponent,
    ToolbarComponent,
    HomeComponent,
    RegisterComponent,
    CardPrintComponent,
    AdminMenuComponent,
    OpenUserManagementComponent,
    GeneralMenuComponent,
    GeneralMenuComponent,
    AddProjectComponent,
    ListProjectComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    FlexLayoutModule,
    HttpClientModule,
    FormsModule,
    DragDropModule,
    ToastrModule.forRoot(),
    FontAwesomeModule,
    NgxPrintModule,
  ],
   entryComponents: [
    OpenCardComponent,
    OpenForAddCardComponent,
    FilePreviewOverlayComponent,
    CardPrintComponent,
    OpenUserManagementComponent,
    AddProjectComponent,
     ListProjectComponent
  ],

  providers: [
    FilePreviewOverlayService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
    provide: HTTP_INTERCEPTORS,
     useClass: HttpErrorInterceptor,
     multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
