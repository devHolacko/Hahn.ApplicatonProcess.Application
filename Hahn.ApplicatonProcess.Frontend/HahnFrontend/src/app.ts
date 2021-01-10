import { RouterConfiguration, Router } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";
import { I18N } from "aurelia-i18n";

export class App {
  public currentUrl = window.location.origin;
  router: Router;
  localization: I18N;
  static inject = [I18N];
  public currentLocale: string;
  constructor(i18n: I18N) {
    this.localization = i18n;
    const currentLanguage = localStorage.getItem("lang");
    console.log({ currentLanguage });
    if (currentLanguage == null) {
      this.currentLocale = "en";
      localStorage.setItem("lang", this.currentLocale);
      i18n.setLocale(this.currentLocale).then();
    } else {
      this.currentLocale = currentLanguage;
      i18n.setLocale(currentLanguage).then();
    }
  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    config.title = "Hahn";
    config.map([
      {
        route: "applicants", redirect: "applicants/list"
      },
      {
        route: "applicants/list", name: "applicants/list",
        moduleId: PLATFORM.moduleName("./applicants/list/applicants-list"), title: "Applicants List"
      },
      {
        route: "applicants/create", name: "applicants/create",
        moduleId: PLATFORM.moduleName("./applicants/create/create-applicant"), title: "Create Applicant"
      },
      {
        route: "applicants/:id/edit", name: "applicants/:id/edit",
        moduleId: PLATFORM.moduleName("./applicants/edit/edit-applicant"), title: "Edit Applicant"
      },
      {
        route: "",
        redirect: "applicants/list"
      }
    ]);
    this.router = router;
  }

  public setLanguage(language: string): void {
    localStorage.setItem("lang", language);
    this.localization.setLocale(language).then();
    location.reload();
  }
}
