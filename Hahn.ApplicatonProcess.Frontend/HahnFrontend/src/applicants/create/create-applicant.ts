import { inject, NewInstance } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";
import { CountryService } from "../../services/country-service";
import { ValidationControllerFactory, ValidationRules, ValidationController } from "aurelia-validation";
import { KeyValuePair } from "models/general/key-value-pair";
import { CreateApplicantRequest } from "models/applicants/request/create-applicant";
import { I18N } from "aurelia-i18n";

@inject(ApplicantService, CountryService, ValidationControllerFactory, I18N)
export class CreateApplicant {
  private readonly applicantService: ApplicantService;
  private readonly countryService: CountryService;
  public createApplicantRequest: CreateApplicantRequest = new CreateApplicantRequest();
  public countriesLookup: KeyValuePair<string, string>[] = [];
  public validationControllerFactory: ValidationController;
  private readonly i18n: I18N;

  constructor(_applicantService: ApplicantService, _countryService: CountryService, controller: ValidationControllerFactory, _i18n: I18N) {
    this.applicantService = _applicantService;
    this.countryService = _countryService;
    this.i18n = _i18n;
    this.createApplicantRequest.hired = false;
    this.validationControllerFactory = controller.createForCurrentScope();
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
      .on(this.createApplicantRequest);
  }

  activate(): void {

    this.countryService.getCountries().then(result => {
      this.countriesLookup = result.data;
    });
  }

  public submit(): void {
    this.applicantService.createApplicant(this.createApplicantRequest).then(result => {
      console.log({ result });
    });
  }

  public reset(): void {
    this.createApplicantRequest.address = "";
    this.createApplicantRequest.age = null;
    this.createApplicantRequest.countryOfOrigin = "";
    this.createApplicantRequest.emailAddress = "";
    this.createApplicantRequest.familyName = "";
    this.createApplicantRequest.hired = false;
    this.createApplicantRequest.name = "";
  }
}
