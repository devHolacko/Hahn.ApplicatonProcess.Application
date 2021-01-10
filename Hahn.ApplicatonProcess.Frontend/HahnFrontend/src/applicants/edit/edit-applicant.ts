// import { Router, activationStrategy } from 'aurelia-router';
// import { inject, NewInstance } from "aurelia-framework";
// import { ApplicantService } from "services/applicant-service";
// import { CountryService } from "../../services/country-service";
// import { ValidationControllerFactory, ValidationRules, ValidationController } from "aurelia-validation";
// import { KeyValuePair } from "models/general/key-value-pair";
// import { EditApplicantRequest } from "models/applicants/request/edit-applicant";
// import { I18N } from "aurelia-i18n";

// @inject(ApplicantService, CountryService, ValidationControllerFactory, I18N, Router)
// export class EditApplicant {
//   router: Router;
//   applicantId: number;
//   private readonly applicantService: ApplicantService;
//   private readonly countryService: CountryService;
//   public editApplicantRequest: EditApplicantRequest = new EditApplicantRequest();
//   public countriesLookup: KeyValuePair<string, string>[] = [];
//   public validationControllerFactory: ValidationController;
//   private readonly i18n: I18N;
//   constructor(router: Router, _applicantService: ApplicantService, _countryService: CountryService, controller: ValidationControllerFactory,
//     _i18n: I18N) {
//     this.applicantService = _applicantService;
//     this.countryService = _countryService;
//     this.i18n = _i18n;
//     this.router = router;
//     // this.validationControllerFactory = controller.createForCurrentScope();
//     ValidationRules
//       .ensure("name").minLength(5).withMessage(this.i18n.tr("Messages.NameLength", { length: 5 })).required().withMessage(this.i18n.tr("Messages.NameRequired"))
//       .ensure("familyName").minLength(5).withMessage(this.i18n.tr("Messages.FamilyNameLength", { length: 5 }))
//       .required().withMessage(this.i18n.tr("Messages.FamilyNameRequired"))
//       .ensure("emailAddress")
//       .required().withMessage(this.i18n.tr("Messages.EmailRequired")).email().withMessage(this.i18n.tr("Messages.EmailFormatInvalid"))
//       .ensure("address").required().withMessage(this.i18n.tr("Messages.AddressRequired")).minLength(10).withMessage(this.i18n.tr("Messages.AddressLength", { length: 5 }))
//       .ensure("countryOfOrigin").required().withMessage(this.i18n.tr("Messages.CountryRequired"))
//       .ensure("age").required().withMessage(this.i18n.tr("Messages.AgeRequired"))
//       .min(20).withMessage(this.i18n.tr("Messages.AgeMin", { minAge: 20 }))
//       .max(60).withMessage(this.i18n.tr("Messages.AgeMax", { AgeMax: 60 }))
//       .on(this.editApplicantRequest);
//   }
//   public activate(params: any): void {
//     this.applicantId = params.id;
//     this.applicantService.getApplicant(this.applicantId).then(applicantResult => {
//       this.countryService.getCountries().then(countriesResult => {
//         this.countriesLookup = countriesResult.data;
//         this.editApplicantRequest = {
//           id: applicantResult.data.id,
//           name: applicantResult.data.name,
//           familyName: applicantResult.data.familyName,
//           address: applicantResult.data.address,
//           age: applicantResult.data.age,
//           countryOfOrigin: applicantResult.data.countryOfOrigin,
//           emailAddress: applicantResult.data.emailAddress,
//           hired: applicantResult.data.hired
//         };
//       });
//     });
//   }

//   public submit(): void {
//     this.applicantService.editApplicant(this.editApplicantRequest).then(result => {
//       console.log({ result });
//     });
//   }

//   public reset(): void {
//     this.editApplicantRequest.address = "";
//     this.editApplicantRequest.age = null;
//     this.editApplicantRequest.countryOfOrigin = "";
//     this.editApplicantRequest.emailAddress = "";
//     this.editApplicantRequest.familyName = "";
//     this.editApplicantRequest.hired = false;
//     this.editApplicantRequest.name = "";
//   }
// }

import { inject, NewInstance } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";
import { CountryService } from "../../services/country-service";
import { ValidationControllerFactory, ValidationRules, ValidationController } from "aurelia-validation";
import { KeyValuePair } from "models/general/key-value-pair";
import { EditApplicantRequest } from "models/applicants/request/edit-applicant";
import { I18N } from "aurelia-i18n";
import { Router, activationStrategy } from 'aurelia-router';

@inject(ApplicantService, CountryService, ValidationControllerFactory, I18N, Router)
export class EditApplicant {
  private readonly applicantService: ApplicantService;
  private readonly countryService: CountryService;
  public editApplicantRequest: EditApplicantRequest = new EditApplicantRequest();
  public countriesLookup: KeyValuePair<string, string>[] = [];
  public validationControllerFactory: ValidationController;
  private readonly i18n: I18N;
  private readonly router: Router;
  applicantId: number;

  constructor(_applicantService: ApplicantService, _countryService: CountryService, controller: ValidationControllerFactory, _i18n: I18N,
    _router: Router) {
    this.applicantService = _applicantService;
    this.countryService = _countryService;
    this.i18n = _i18n;
    this.router = _router;
    this.validationControllerFactory = controller.createForCurrentScope();

  }

  activate(params: any): void {
    this.applicantId = params.id;
    this.applicantService.getApplicant(this.applicantId).then(applicantResult => {
      this.countryService.getCountries().then(countriesResult => {
        this.countriesLookup = countriesResult.data;
        this.editApplicantRequest = {
          id: applicantResult.data.id,
          name: applicantResult.data.name,
          familyName: applicantResult.data.familyName,
          address: applicantResult.data.address,
          age: applicantResult.data.age,
          countryOfOrigin: applicantResult.data.countryOfOrigin,
          emailAddress: applicantResult.data.emailAddress,
          hired: applicantResult.data.hired
        };
        ValidationRules
          .ensure("name").minLength(5).withMessage(this.i18n.tr("Messages.NameLength", { length: 5 })).required().withMessage(this.i18n.tr("Messages.NameRequired"))
          .ensure("familyName").minLength(5).withMessage(this.i18n.tr("Messages.FamilyNameLength", { length: 5 }))
          .required().withMessage(this.i18n.tr("Messages.FamilyNameRequired"))
          .ensure("emailAddress")
          .required().withMessage(this.i18n.tr("Messages.EmailRequired")).email().withMessage(this.i18n.tr("Messages.EmailFormatInvalid"))
          .ensure("address").required().withMessage(this.i18n.tr("Messages.AddressRequired")).minLength(10).withMessage(this.i18n.tr("Messages.AddressLength", { length: 5 }))
          .ensure("countryOfOrigin").required().withMessage(this.i18n.tr("Messages.CountryRequired"))
          .ensure("age").required().withMessage(this.i18n.tr("Messages.AgeRequired"))
          .min(20).withMessage(this.i18n.tr("Messages.AgeMin", { minAge: 20 }))
          .max(60).withMessage(this.i18n.tr("Messages.AgeMax", { AgeMax: 60 }))
          .on(this.editApplicantRequest);
      });
    });
  }

  public submit(): void {
    this.applicantService.editApplicant(this.editApplicantRequest).then(result => {
      console.log({ result });
    });
  }

  public cancel(): void {
    this.editApplicantRequest.address = "";
    this.editApplicantRequest.age = null;
    this.editApplicantRequest.countryOfOrigin = "";
    this.editApplicantRequest.emailAddress = "";
    this.editApplicantRequest.familyName = "";
    this.editApplicantRequest.hired = false;
    this.editApplicantRequest.name = "";
  }
}
