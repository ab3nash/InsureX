import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { MonthlyPremiumsComponent } from './components/monthly-premiums/monthly-premiums.component';
import { ApplicantDetailsComponent } from './components/applicant-details/applicant-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MonthlyPremiumsComponent,
    ApplicantDetailsComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
