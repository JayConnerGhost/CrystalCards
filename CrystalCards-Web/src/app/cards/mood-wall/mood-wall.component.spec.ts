import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MoodWallComponent } from './mood-wall.component';

describe('MoodWallComponent', () => {
  let component: MoodWallComponent;
  let fixture: ComponentFixture<MoodWallComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoodWallComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoodWallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
