import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ApplicantConfig } from '../ApplicantConfig';

@Injectable({
  providedIn: 'root'
})
export class ApplicantConfigService {
  private apiUrl: string = 'http://localhost:5273/applicant/Config';

  constructor(private http:HttpClient) { }

  getApplicantConfig() : Observable<ApplicantConfig> {
    return this.http.get<ApplicantConfig>(this.apiUrl);
  }
}
