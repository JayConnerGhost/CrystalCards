import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolbarComponent } from './toolbar.component';
import {HomeComponent} from '../home/home.component';
import {LoginComponent} from '../login/login.component';
import {CardListComponent} from '../cards/card-list/card-list.component';
import {ProjectPictureComponent} from '../projects/project-picture/project-picture.component';
import {CardTokenComponent} from '../cards/card-token/card-token.component';
import {CardsListViewComponent} from '../cards/cards-list-view/cards-list-view.component';
import {AdminMenuComponent} from '../users/admin-menu/admin-menu.component';
import {GeneralMenuComponent} from '../users/general-menu/general-menu.component';
import {AddCardComponent} from '../cards/add-card/add-card.component';
import {FooterComponent} from '../footer/footer.component';
import {AppComponent} from '../app.component';
import {RegisterComponent} from '../register/register.component';
import {RouterModule, Routes} from '@angular/router';
import {BrowserModule, By} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MaterialModule} from '../../MaterialModule';
import {AppRoutingModule} from '../app-routing.module';
import {FlexLayoutModule} from '@angular/flex-layout';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {ToastrModule} from 'ngx-toastr';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {NgxPrintModule} from 'ngx-print';
import {MatSelectModule} from '@angular/material/select';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSortModule} from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTabsModule} from '@angular/material/tabs';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatTableModule} from '@angular/material/table';
import {MatMenuModule} from '@angular/material/menu';
import {DebugElement} from '@angular/core';
import {AuthGuard} from '../guards/auth.guard';

describe('ToolbarComponent', () => {
  let component: ToolbarComponent;
  let fixture: ComponentFixture<ToolbarComponent>;

  const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'login', component: LoginComponent},
    {path: 'cards', component: CardListComponent, canActivate: [AuthGuard]},
    {path: 'projects', component: ProjectPictureComponent, canActivate: [AuthGuard]}
  ];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        HomeComponent,
        LoginComponent,
        CardListComponent,
        ProjectPictureComponent,
        CardTokenComponent,
        CardsListViewComponent,
        ToolbarComponent,
        AdminMenuComponent,
        AdminMenuComponent,
        GeneralMenuComponent,
        AddCardComponent,
        FooterComponent,
        AppComponent,
        RegisterComponent
      ],
      imports: [
        RouterModule.forRoot(routes),
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
        MatSelectModule,
        MatTooltipModule,
        MatSortModule,
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
        MatTableModule,
        MatMenuModule,

      ]
    })
      .compileComponents();
  }));

  it('Should have a welcome message', async(() => {
    const fixture = TestBed.createComponent(ToolbarComponent);
    fixture.detectChanges();
    const debugElement = fixture.debugElement.query(By.css('#title'));
    expect(debugElement.nativeElement.textContent).toEqual('Crystal Ideas');
  }));

});
