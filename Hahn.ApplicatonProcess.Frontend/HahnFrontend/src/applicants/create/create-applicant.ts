import { inject } from "aurelia-framework";
import { KeyValuePair } from 'models/general/key-value-pair';
import { CountryService } from '../../services/country-service';
@inject(CountryService)
export class CreateApplicant {
  countryService: CountryService;
  countriesLookup: KeyValuePair<string, string>[] = [];
  constructor(_countryService: CountryService) {
    this.countryService = _countryService;
  }

  activate() {
    this.countryService.getCountries().then(result => {
      this.countriesLookup = result.data;
    });
  }
}
