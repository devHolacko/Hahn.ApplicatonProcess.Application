import { inject, NewInstance } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";
import { CountryService } from '../../services/country-service'
import { ValidationControllerFactory, ValidationRules, ValidationController } from 'aurelia-validation';
import { KeyValuePair } from 'models/general/key-value-pair';
import { CreateApplicantRequest } from 'models/applicants/request/create-applicant';

@inject(ApplicantService, CountryService, ValidationControllerFactory)
export class CreateApplicant {
  private readonly applicantService: ApplicantService;
  private readonly countryService: CountryService;
  public createApplicantRequest: CreateApplicantRequest = new CreateApplicantRequest();
  public countriesLookup: KeyValuePair<string, string>[] = [];
  public validationControllerFactory: ValidationController;

  constructor(_applicantService: ApplicantService, _countryService: CountryService, controller: ValidationControllerFactory) {
    this.applicantService = _applicantService;
    this.countryService = _countryService;
    this.createApplicantRequest.hired = false;
    this.validationControllerFactory = controller.createForCurrentScope();
    ValidationRules
      .ensure('name').minLength(5).required()
      .ensure('familyName').minLength(5).required()
      .ensure('emailAddress').minLength(5).required().email()
      .ensure('address').required().minLength(10)
      .ensure('countryOfOrigin').required()
      .ensure('age').required().min(20).max(60)
      .on(this.createApplicantRequest);
  }

  activate(params, routeConfig, navigationInstruction) {

    this.countryService.getCountries().then(result => {
      this.countriesLookup = result.data;
    });
  }

  public submit(): void {
    console.log('request', this.createApplicantRequest);

  }

  public reset(): void {
    this.createApplicantRequest.address = '';
    this.createApplicantRequest.age = null;
    this.createApplicantRequest.countryOfOrigin = '';
    this.createApplicantRequest.emailAddress = '';
    this.createApplicantRequest.familyName = '';
    this.createApplicantRequest.hired = false;
    this.createApplicantRequest.name = '';
  }
}
