import { inject } from "aurelia-framework";
import { Router } from 'aurelia-router';

@inject(Router)
export class RequestConfirmation {
  private router: Router;
  constructor(_router: Router) {
    this.router = _router;
  }

  goToApplicantsList() {
    this.router.navigate("applicants/list");
  }

  goToCreateApplicants() {
    this.router.navigate("applicants/create");
  }
}
