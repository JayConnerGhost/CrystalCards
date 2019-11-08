import { TestBed } from '@angular/core/testing';

import { ProjectApiServiceService } from './project-api-service.service';

describe('ProjectApiServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectApiServiceService = TestBed.get(ProjectApiServiceService);
    expect(service).toBeTruthy();
  });
});
