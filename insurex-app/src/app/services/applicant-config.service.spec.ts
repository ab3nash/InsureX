import { TestBed } from '@angular/core/testing';

import { ApplicantConfigService } from './applicant-config.service';

describe('ApplicantConfigService', () => {
  let service: ApplicantConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApplicantConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
