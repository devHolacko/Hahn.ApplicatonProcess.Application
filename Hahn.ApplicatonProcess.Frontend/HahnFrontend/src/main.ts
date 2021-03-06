import { Aurelia } from 'aurelia-framework';
import * as environment from '../config/environment.json';
import { PLATFORM } from 'aurelia-pal';
import { TCustomAttribute } from 'aurelia-i18n';
import XHR from 'i18next-xhr-backend';

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'), config => {
      config.useDefaults();
      config.settings.overlayDismiss = false;
      config.settings.centerHorizontalOnly = false;
      config.settings.startingZIndex = 5;
      config.settings.keyboard = true;
    })
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
      const aliases = ['t', 'i18n'];
      // add aliases for 't' attribute
      TCustomAttribute.configureAliases(aliases);

      // register backend plugin
      // instance.i18next.use(Backend.with(aurelia.loader));
      // instance.i18next.use(Backend);
      instance.i18next.use(XHR);

      // adapt options to your needs (see http://i18next.com/docs/options/)
      // make sure to return the promise of the setup method, in order to guarantee proper loading
      return instance.setup({
        backend: {
          loadPath: 'locales/{{lng}}/{{ns}}.json',
        },
        attributes: aliases,
        lng: 'de',
        fallbackLng: 'en',
        ns: ['translation'],
        defaultNs: 'translation',
        debug: false
      });
    });

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  // aurelia.use.plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
  //   const aliases = ['t', 'i18n'];
  //   // add aliases for 't' attribute
  //   TCustomAttribute.configureAliases(aliases);

  //   // register backend plugin
  //   // instance.i18next.use(Backend.with(aurelia.loader));
  //   instance.i18next.use(Backend);
  //   // instance.i18next.use(XHR);

  //   // adapt options to your needs (see http://i18next.com/docs/options/)
  //   // make sure to return the promise of the setup method, in order to guarantee proper loading
  //   return instance.setup({
  //     fallbackLng: 'du', // <------------ 6
  //     whitelist: ['en', 'du'],
  //     preload: ['en', 'du'], // <------------ 7
  //     ns: 'global', // <------------ 8
  //     defaultNS: 'global',
  //     fallbackNS: false,
  //     attributes: aliases, // <------------ 9
  //     lng: 'en', // <------------ 10
  //     debug: true, // <------------ 11
  //     backend: {
  //       loadPath: 'src/locales/{{lng}}/{{ns}}.json',
  //     }
  //   });
  // });

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
