import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardPrintComponent } from './card-print.component';

describe('CardPrintComponent', () => {
  let component: CardPrintComponent;
  let fixture: ComponentFixture<CardPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
