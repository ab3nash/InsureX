import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CalculatedPremium } from '../CalculatedPremium';
import { ApplicantDetails } from '../ApplicantDetails';
import {Environment, ApiPaths} from '../Environment';

@Injectable({
  providedIn: 'root'
})
export class PremiumCalculationService {
  private apiUrl: string = Environment.baseUrl + ApiPaths.CalculatePremium;

  constructor(private http: HttpClient) { }

  getCalculatedPremium(applicantDetails: ApplicantDetails): Observable<CalculatedPremium> {
    var url = new URL(this.apiUrl);
    url.searchParams.set('name', applicantDetails.name);
    url.searchParams.set('dateOfBirth', applicantDetails.dateOfBirth.format());
    url.searchParams.set('occupation', applicantDetails.occupation);
    url.searchParams.set('sumInsured', applicantDetails.sumInsured.toString());

    return this.http.get<CalculatedPremium>(url.toString());
  }
}