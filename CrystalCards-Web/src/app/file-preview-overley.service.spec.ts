import { TestBed } from '@angular/core/testing';

import { FilePreviewOverleyService } from './file-preview-overley.service';

describe('FilePreviewOverleyService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FilePreviewOverleyService = TestBed.get(FilePreviewOverleyService);
    expect(service).toBeTruthy();
  });
});
