import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenUserManagementComponent } from './open-user-management.component';

describe('OpenUserManagementComponent', () => {
  let component: OpenUserManagementComponent;
  let fixture: ComponentFixture<OpenUserManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenUserManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenUserManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
