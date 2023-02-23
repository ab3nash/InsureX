import { Component } from '@angular/core';
import { CalculatedPremium } from '../../CalculatedPremium';

@Component({
  selector: 'app-monthly-premiums',
  templateUrl: './monthly-premiums.component.html',
  styleUrls: ['./monthly-premiums.component.css']
})
export class MonthlyPremiumsComponent {
  calculatedPremium: CalculatedPremium = {
    MonthlyDeathPremium : 2000,
    MonthlyTpdPremium : 1900
  };
}