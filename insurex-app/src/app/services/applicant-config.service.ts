import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ApplicantConfig } from '../ApplicantConfig';
import {Environment, ApiPaths} from '../Environment';

@Injectable({
  providedIn: 'root'
})
export class ApplicantConfigService {
  private apiUrl: string = Environment.baseUrl + ApiPaths.GetApplicantConfig;

  constructor(private http:HttpClient) { }

  getApplicantConfig() : Observable<ApplicantConfig> {
    return this.http.get<ApplicantConfig>(this.apiUrl);
  }
}
