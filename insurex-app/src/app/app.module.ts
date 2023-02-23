import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {DpDatePickerModule} from 'ng2-date-picker';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { MonthlyPremiumsComponent } from './components/monthly-premiums/monthly-premiums.component';
import { ApplicantDetailsComponent } from './components/applicant-details/applicant-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MonthlyPremiumsComponent,
    ApplicantDetailsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    DpDatePickerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
