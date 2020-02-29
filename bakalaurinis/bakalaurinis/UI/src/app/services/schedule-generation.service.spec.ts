import { TestBed } from '@angular/core/testing';

import { ScheduleGenerationService } from './schedule-generation.service';

describe('ScheduleGenerationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ScheduleGenerationService = TestBed.get(ScheduleGenerationService);
    expect(service).toBeTruthy();
  });
});
