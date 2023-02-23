import { Component, OnInit } from '@angular/core';
import * as dayjs from 'dayjs';
import { Dayjs } from 'dayjs';
import { ApplicantConfig } from 'src/app/ApplicantConfig';
import { ApplicantDetails } from 'src/app/ApplicantDetails';
import { ApplicantConfigService } from 'src/app/services/applicant-config.service';

@Component({
  selector: 'app-applicant-details',
  templateUrl: './applicant-details.component.html',
  styleUrls: ['./applicant-details.component.css']
})
export class ApplicantDetailsComponent implements OnInit {
  applicantConfig: ApplicantConfig = { maxAge: 0, occupations: [] };
  applicantDetails: ApplicantDetails = {
    name: '',
    age: 0,
    dateOfBirth: dayjs(),
    occupation: '',
    sumInsured: 0
  };

  constructor(private applicantConfigService: ApplicantConfigService) { }

  ngOnInit(): void {
    this.applicantConfigService.getApplicantConfig().subscribe((applicantConfig) => this.applicantConfig = applicantConfig);
  }

  onOccupationChange(): void {
  }

  onDateOfBirthChange(): void{
    this.applicantDetails.age = dayjs().diff(this.applicantDetails.dateOfBirth, 'year');
  }
}
