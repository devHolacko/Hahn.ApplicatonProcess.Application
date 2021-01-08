import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';

export class App {
  public message = 'Hello World!';
  public currentUrl = window.location.href;
  router: Router;
  configureRouter(config: RouterConfiguration, router: Router): void {
    config.title = 'Hahn';
    config.map([
      {
        route: 'applicants', redirect: 'applicants/list'
      },
      {
        route: 'applicants/list', name: 'applicants/list',
        moduleId: PLATFORM.moduleName('./applicants/list/applicants-list'), title: 'Applicants List'
      },
      {
        route: 'applicants/create', name: 'applicants/create',
        moduleId: PLATFORM.moduleName('./applicants/create/create-applicant'), title: 'Create Applicant'
      },
      {
        route: 'applicants/:id/edit', name: 'applicants/:id/edit',
        moduleId: PLATFORM.moduleName('./applicants/edit/edit-applicant'), title: 'Edit Applicant'
      },
      {
        route: '',
        redirect: 'applicants/list'
      }
    ]);
    this.router = router;
  }
}
