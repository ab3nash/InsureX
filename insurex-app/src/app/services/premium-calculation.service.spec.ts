import { TestBed } from '@angular/core/testing';

import { PremiumCalculationService } from './premium-calculation.service';

describe('PremiumCalculationService', () => {
  let service: PremiumCalculationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PremiumCalculationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
