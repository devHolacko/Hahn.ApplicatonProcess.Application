import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';

export class App {
  public message = 'Hello World!';
  router: Router;
  configureRouter(config: RouterConfiguration, router: Router): void {
    config.title = 'Title';
    config.map([
      {
        route: 'applicans', redirect: 'applicants/list'
      },
      {
        route: 'applicants/list', name: 'applicants/list',
        moduleId: PLATFORM.moduleName('./applicants/list/applicants-list'), title: 'Applicants'
      },
      {
        route: 'applicants/create', name: 'applicants/create',
        moduleId: PLATFORM.moduleName('./applicants/create/create-applicant'), title: 'Create'
      },
      {
        route: '',
        redirect: 'applicants/list'
      }
    ]);
    this.router = router;
  }
}
