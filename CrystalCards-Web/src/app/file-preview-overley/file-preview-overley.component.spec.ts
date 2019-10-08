import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilePreviewOverleyComponent } from './file-preview-overley.component';

describe('FilePreviewOverleyComponent', () => {
  let component: FilePreviewOverleyComponent;
  let fixture: ComponentFixture<FilePreviewOverleyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilePreviewOverleyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilePreviewOverleyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
