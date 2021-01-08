import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';

export class App {
  public message = 'Hello World!';
  router: Router;
  configureRouter(config: RouterConfiguration, router: Router): void {
    config.title = 'Title';
    config.map([
      {
        route: 'applicants', name: 'applicants',
        moduleId: PLATFORM.moduleName('./applicants/list/applicants-list'), title: 'Applicants'
      },
      {
        route: '',
        redirect: 'applicants'
      }
    ]);
    this.router = router;
  }
}
