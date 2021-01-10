import { inject } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";
import { CountryService } from "../../services/country-service";
import { ValidationControllerFactory, ValidationRules, ValidationController } from "aurelia-validation";
import { KeyValuePair } from "models/general/key-value-pair";
import { EditApplicantRequest } from "models/applicants/request/edit-applicant";
import { I18N } from "aurelia-i18n";
import { Router } from 'aurelia-router';
import { DialogService } from 'aurelia-dialog';
import { ErrorDialog } from 'applicants/error-dialog/error-dialog';

@inject(ApplicantService, CountryService, ValidationControllerFactory, I18N, Router, DialogService)
export class EditApplicant {
  private readonly applicantService: ApplicantService;
  private readonly countryService: CountryService;
  public editApplicantRequest: EditApplicantRequest = new EditApplicantRequest();
  public countriesLookup: KeyValuePair<string, string>[] = [];
  public validationControllerFactory: ValidationController;
  private readonly i18n: I18N;
  private readonly router: Router;
  applicantId: number;
  public loading = false;
  public apiErrorMessages: string[] = [];
  private readonly dialogService: DialogService;

  constructor(_applicantService: ApplicantService, _countryService: CountryService, controller: ValidationControllerFactory, _i18n: I18N,
    _router: Router, _dialogService: DialogService) {
    this.applicantService = _applicantService;
    this.countryService = _countryService;
    this.i18n = _i18n;
    this.router = _router;
    this.validationControllerFactory = controller.createForCurrentScope();
    this.dialogService = _dialogService;
  }

  activate(params: any): void {
    this.loading = true;
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
          .ensure("address").required().withMessage(this.i18n.tr("Messages.AddressRequired")).minLength(10).withMessage(this.i18n.tr("Messages.AddressLength", { length: 10 }))
          .ensure("countryOfOrigin").required().withMessage(this.i18n.tr("Messages.CountryRequired"))
          .ensure("age").required().withMessage(this.i18n.tr("Messages.AgeRequired"))
          .min(20).withMessage(this.i18n.tr("Messages.AgeMin", { minAge: 20 }))
          .max(60).withMessage(this.i18n.tr("Messages.AgeMax", { AgeMax: 60 }))
          .on(this.editApplicantRequest);
        this.loading = false;
      });
    });
  }

  public submit(): void {
    this.loading = true;
    this.apiErrorMessages = [];
    this.applicantService.editApplicant(this.editApplicantRequest).then(result => {
      this.loading = false;
      if (result && result.success) {
        this.router.navigate("applicants/request-confirmation");
      } else {
        this.apiErrorMessages = [];
        const mappedResult: KeyValuePair<string, string>[] = result.data;
        mappedResult.map((item, index) => {
          this.apiErrorMessages.push(item.value);
        });

        this.dialogService.open({ viewModel: ErrorDialog, model: this.apiErrorMessages });
      }
    });
  }

  public cancel(): void {
    this.router.navigate("applicants/list");
  }

  public get isFormTouched(): boolean {
    if (
      (this.editApplicantRequest.address !== "" && this.editApplicantRequest.address !== undefined) ||
      this.editApplicantRequest.age !== undefined ||
      (this.editApplicantRequest.countryOfOrigin !== "" && this.editApplicantRequest.countryOfOrigin !== undefined) ||
      (this.editApplicantRequest.emailAddress !== "" && this.editApplicantRequest.emailAddress !== undefined) ||
      (this.editApplicantRequest.familyName !== "" && this.editApplicantRequest.familyName !== undefined) ||
      (this.editApplicantRequest.name !== "" && this.editApplicantRequest.name !== undefined)
    ) {
      return true;
    } else {
      return false;
    }
  }

  public get isFormValid(): boolean {
    if (
      (this.editApplicantRequest.address !== "" && this.editApplicantRequest.address !== undefined) &&
      this.editApplicantRequest.age !== undefined &&
      (this.editApplicantRequest.countryOfOrigin !== "" && this.editApplicantRequest.countryOfOrigin !== undefined) &&
      (this.editApplicantRequest.emailAddress !== "" && this.editApplicantRequest.emailAddress !== undefined) &&
      (this.editApplicantRequest.familyName !== "" && this.editApplicantRequest.familyName !== undefined) &&
      (this.editApplicantRequest.name !== "" && this.editApplicantRequest.name !== undefined)
      && this.validationControllerFactory.errors.length === 0
    ) {
      return true;
    } else {
      return false;
    }
  }
}
