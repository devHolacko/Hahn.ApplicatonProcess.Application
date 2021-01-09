import { inject } from "aurelia-framework";
import { KeyValuePair } from 'models/general/key-value-pair';
import { CountryService } from '../../services/country-service';
import { CreateApplicantRequest } from '../../models/applicants/request/create-applicant';
import { ApplicantService } from 'services/applicant-service';

@inject(ApplicantService, CountryService)
export class CreateApplicant {
  createApplicantRequest: CreateApplicantRequest = null;
  countryService: CountryService;
  applicantService: ApplicantService;
  countriesLookup: KeyValuePair<string, string>[] = [];

  constructor(_countryService: CountryService, _applicantService: ApplicantService) {
    this.countryService = _countryService;
    this.applicantService = _applicantService;
  }

  activate() {
    this.countryService.getCountries().then(result => {
      this.countriesLookup = result.data;
    });
  }
}
