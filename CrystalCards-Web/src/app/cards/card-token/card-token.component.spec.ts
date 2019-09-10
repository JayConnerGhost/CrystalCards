import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardTokenComponent } from './card-token.component';

describe('CardTokenComponent', () => {
  let component: CardTokenComponent;
  let fixture: ComponentFixture<CardTokenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardTokenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardTokenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
