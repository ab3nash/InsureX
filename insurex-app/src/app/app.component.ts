import { Component } from '@angular/core';
import { ApplicantDetails } from './ApplicantDetails';
import { CalculatedPremium } from './CalculatedPremium';
import { PremiumCalculationService } from './services/premium-calculation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  calculatedPremium: CalculatedPremium = {
    monthlyDeathPremium : 0,
    monthlyTpdPremium : 0
  };

  constructor(private premiumCalculationService: PremiumCalculationService) {

  }

  calculatePremium(applicantDetails: ApplicantDetails) {
    this.premiumCalculationService.getCalculatedPremium(applicantDetails).subscribe(
      (calculatedPremium) => this.calculatedPremium = calculatedPremium);
  }

}
