import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardFullScreenViewComponent } from './card-full-screen-view.component';

describe('CardFullScreenViewComponent', () => {
  let component: CardFullScreenViewComponent;
  let fixture: ComponentFixture<CardFullScreenViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardFullScreenViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardFullScreenViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
