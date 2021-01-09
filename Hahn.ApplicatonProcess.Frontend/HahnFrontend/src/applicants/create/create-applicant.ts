import { inject } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";
import { CountryService } from '../../services/country-service'
import { ValidationController, ValidationControllerFactory, ValidationRules } from 'aurelia-validation';
import { KeyValuePair } from 'models/general/key-value-pair';
import { CreateApplicantRequest } from 'models/applicants/request/create-applicant';

@inject(ApplicantService, CountryService, ValidationControllerFactory)
export class CreateApplicant {
  private readonly applicantService: ApplicantService;
  private readonly countryService: CountryService;
  private controller: ValidationController;
  public createApplicantRequest: CreateApplicantRequest = new CreateApplicantRequest();
  public countriesLookup: KeyValuePair<string, string>[] = [];
  constructor(_applicantService: ApplicantService, _countryService: CountryService, controllerFactor: ValidationControllerFactory) {
    this.applicantService = _applicantService;
    this.countryService = _countryService;
    this.controller = controllerFactor.createForCurrentScope();
  }

  activate(params, routeConfig, navigationInstruction) {
    this.countryService.getCountries().then(result => {
      this.countriesLookup = result.data;
    });
    ValidationRules
      .ensure('createApplicantRequest.name').required()
      .on(this);
    ValidationRules
      .ensure('createApplicantRequest.familyName').required()
      .on(this);
    ValidationRules
      .ensure('createApplicantRequest.address').required()
      .on(this);
    ValidationRules
      .ensure('createApplicantRequest.country').required()
      .on(this);
    ValidationRules
      .ensure('createApplicantRequest.email').required()
      .on(this);
    ValidationRules
      .ensure('createApplicantRequest.age').required()
      .on(this);
  }

  public submit() {
    console.log(JSON.stringify(this.createApplicantRequest));
  }
}
