import { Router, activationStrategy } from 'aurelia-router';

export class EditApplicant {
  router: Router;
  applicantId: number;
  constructor(router: Router) {
    this.router = router;
  }
  public activate(params: any): void {
    this.applicantId = params.id;
  }
}
