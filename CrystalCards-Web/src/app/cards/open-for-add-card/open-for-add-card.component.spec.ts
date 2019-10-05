import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenForAddCardComponent } from './open-for-add-card.component';

describe('OpenForAddCardComponent', () => {
  let component: OpenForAddCardComponent;
  let fixture: ComponentFixture<OpenForAddCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenForAddCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenForAddCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
