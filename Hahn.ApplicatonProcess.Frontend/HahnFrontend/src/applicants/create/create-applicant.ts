// import { inject } from "aurelia-framework";
// import { KeyValuePair } from 'models/general/key-value-pair';
// import { CountryService } from '../../services/country-service';
// import { CreateApplicantRequest } from '../../models/applicants/request/create-applicant';
// import { ApplicantService } from 'services/applicant-service';
// import { ValidationController, ValidationControllerFactory, ValidationRules } from 'aurelia-validation';

// @inject(ApplicantService, CountryService)
// export class CreateApplicant {
//   createApplicantRequest: CreateApplicantRequest = new CreateApplicantRequest();
//   public countryService: CountryService;
//   public applicantService: ApplicantService;
//   countriesLookup: KeyValuePair<string, string>[] = [];
//   controller: ValidationControllerFactory;

//   constructor(_countryService: CountryService, _applicantService: ApplicantService, validationController: ValidationControllerFactory) {
//     this.countryService = _countryService;
//     this.applicantService = _applicantService;
//     // this.controller = validationController;
//     // this.countryService.getCountries().then(result => {
//     //   this.countriesLookup = result.data;
//     // });
//     // const result = _countryService.getAllCountries();
//     // console.log({ result });
//   }

//   created() {
//     // this.countryService.getCountries().then(result => {
//     //   this.countriesLookup = result.data;
//     // });
//     const result = this.applicantService.getApplicant(1);
//     console.log({ result });
//   }

//   activate() {

//     // ValidationRules
//     //   .ensure('createApplicantRequest.name').required()
//     //   .on(this);
//     // ValidationRules
//     //   .ensure('createApplicantRequest.familyName').required()
//     //   .on(this);
//     // ValidationRules
//     //   .ensure('createApplicantRequest.address').required()
//     //   .on(this);
//     // ValidationRules
//     //   .ensure('createApplicantRequest.country').required()
//     //   .on(this);
//     // ValidationRules
//     //   .ensure('createApplicantRequest.email').required()
//     //   .on(this);
//     // ValidationRules
//     //   .ensure('createApplicantRequest.age').required()
//     //   .on(this);
//   }

//   submit() {
//     console.log('request', this.createApplicantRequest);
//   }
// }

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
