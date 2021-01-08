import { Aurelia } from 'aurelia-framework';
import * as environment from '../config/environment.json';
import { PLATFORM } from 'aurelia-pal';
import { I18N, Backend, TCustomAttribute } from 'aurelia-i18n';

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  aurelia.use.plugin('aurelia-i18n', (instance) => {
    const aliases = ['t', 'i18n'];
    // add aliases for 't' attribute
    TCustomAttribute.configureAliases(aliases);

    // register backend plugin
    instance.i18next.use(Backend.with(aurelia.loader));

    // adapt options to your needs (see http://i18next.com/docs/options/)
    // make sure to return the promise of the setup method, in order to guarantee proper loading
    return instance.setup({
      backend: {                                  // <-- configure backend settings
        loadPath: './resources/locales/{{lng}}/{{ns}}.json', // <-- XHR settings for where to get the files from
      },
      attributes: aliases,
      lng: 'de',
      fallbackLng: 'en',
      debug: false
    });
  });

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
