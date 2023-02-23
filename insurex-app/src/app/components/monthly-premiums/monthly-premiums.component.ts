import { Component, Input } from '@angular/core';
import { CalculatedPremium } from '../../CalculatedPremium';

@Component({
  selector: 'app-monthly-premiums',
  templateUrl: './monthly-premiums.component.html',
  styleUrls: ['./monthly-premiums.component.css']
})
export class MonthlyPremiumsComponent {
  @Input() calculatedPremium!: CalculatedPremium;
}