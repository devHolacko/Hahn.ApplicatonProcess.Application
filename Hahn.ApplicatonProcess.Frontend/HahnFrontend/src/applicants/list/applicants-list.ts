import { inject } from "aurelia-framework";
import { ApplicantListItem } from 'models/applicants/response/applicant-list-item';
import { ApplicantService } from "services/applicant-service";
import { Router } from 'aurelia-router';

@inject(ApplicantService, Router)
export class ApplicantsList {
  private readonly applicantService: ApplicantService;
  public applicantsList: ApplicantListItem[] = [];
  public router: Router;
  constructor(_applicantService: ApplicantService, _router: Router) {
    this.applicantService = _applicantService;
    this.router = _router;
  }

  activate(params, routeConfig, navigationInstruction) {
    this.applicantService.getApplicantsList().then(result => {
      this.applicantsList = result.data;
    });
  }

  create() {
    this.router.navigate('applicants/create')
  }

  public navigateToEdit(id: number) {
    this.router.navigate(`applicants/${id}/edit`);
  }
}
