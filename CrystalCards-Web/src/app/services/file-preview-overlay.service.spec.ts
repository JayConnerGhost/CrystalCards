import { TestBed } from '@angular/core/testing';

import { FilePreviewOverlayService } from './file-preview-overlay.service';

describe('FilePreviewOverlayService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FilePreviewOverlayService = TestBed.get(FilePreviewOverlayService);
    expect(service).toBeTruthy();
  });
});
