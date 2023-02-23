import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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
  @Output() onCalculatePremium: EventEmitter<ApplicantDetails> = new EventEmitter();

  applicantConfig: ApplicantConfig = { maxAge: 0, occupations: [] };
  minDate: Dayjs = dayjs();
  maxDate: Dayjs = dayjs().subtract(1, 'year');
  formattedAmount: string = '';
  applicantDetails: ApplicantDetails = {
    name: '',
    age: 0,
    dateOfBirth: dayjs().subtract(1, 'year'),
    occupation: '',
    sumInsured: 0
  };

  constructor(private applicantConfigService: ApplicantConfigService, private currencyPipe: CurrencyPipe) { }

  ngOnInit(): void {
    this.applicantConfigService.getApplicantConfig().subscribe(
      (applicantConfig) => {
        this.applicantConfig = applicantConfig;
        this.minDate = dayjs().subtract(this.applicantConfig.maxAge, 'year');
      });
  }

  onFormChange(): void {
    if (!this.applicantDetails.name ||
      !this.applicantDetails.occupation ||
      this.applicantDetails.sumInsured <= 0 ||
      dayjs().diff(this.applicantDetails.dateOfBirth, 'year') < 1 ||
      dayjs().diff(this.applicantDetails.dateOfBirth, 'year') > this.applicantConfig.maxAge) {
      return;
    }

    this.onCalculatePremium.emit(this.applicantDetails);
  }

  onDateOfBirthChange(): void {
    this.applicantDetails.age = dayjs().diff(this.applicantDetails.dateOfBirth, 'year');
    this.onFormChange();
  }

  transformAmount(element: Event) {
    try {
      if (typeof ((<HTMLInputElement>element.target)?.value) !== 'number')
        this.formattedAmount = this.currencyPipe.transform(this.applicantDetails.sumInsured, 'USD', '$', '1.2') ?? "";
      (<HTMLInputElement>element.target).value = this.formattedAmount;
    } catch (e) {
      this.applicantDetails.sumInsured = 0;
    }

    this.onFormChange();
  }
  removeCurrencyPipeFormat(element: Event) {
    try {
      if ((<HTMLInputElement>element.target)?.value.indexOf('$') !== -1 ||
        (<HTMLInputElement>element.target)?.value.indexOf(',') !== -1) {
        this.formattedAmount = this.formattedAmount.replace('$', '').replace(',', '');
        (<HTMLInputElement>element.target).value = this.formattedAmount;
      } else {
        this.currencyPipe.transform(0, 'USD', '$', '1.2') ?? "";
      }
    } catch (e) {
      this.applicantDetails.sumInsured = 0;
    }
  }

}
