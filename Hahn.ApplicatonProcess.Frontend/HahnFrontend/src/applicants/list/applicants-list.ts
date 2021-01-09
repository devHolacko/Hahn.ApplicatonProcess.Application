import { inject } from "aurelia-framework";
import { ApplicantService } from "services/applicant-service";

@inject(ApplicantService)
export class ApplicantsList {
  private readonly applicantService: ApplicantService;
  constructor(_applicantService: ApplicantService) {
    this.applicantService = _applicantService;
  }

  activate(params, routeConfig, navigationInstruction) {
    this.applicantService.getApplicant(1);
  }
}
