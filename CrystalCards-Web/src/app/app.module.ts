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
    ToolbarComponent
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

   ],
   entryComponents: [
    OpenCardComponent,
    OpenForAddCardComponent,
    FilePreviewOverlayComponent
  ],

  providers: [
    FilePreviewOverlayService,
    {
    provide: HTTP_INTERCEPTORS,
     useClass: HttpErrorInterceptor,
     multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
