import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyPremiumsComponent } from './monthly-premiums.component';

describe('MonthlyPremiumsComponent', () => {
  let component: MonthlyPremiumsComponent;
  let fixture: ComponentFixture<MonthlyPremiumsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyPremiumsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlyPremiumsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
