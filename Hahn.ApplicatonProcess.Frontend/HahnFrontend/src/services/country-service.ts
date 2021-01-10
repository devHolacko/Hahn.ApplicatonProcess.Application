import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import * as environment from "../../config/environment.json";
import { DataGenericResponse } from '../models/general/data-generic-response';
import { KeyValuePair } from '../models/general/key-value-pair';

@inject(HttpClient)
export class CountryService {
  private httpClient: HttpClient;
  private readonly countriesUrl: string;
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.countriesUrl = `${environment.apiUrl}/${environment.countries}`;
  }

  public getCountries() {
    return this.httpClient.get(this.countriesUrl).then(result => {
      const response: DataGenericResponse<KeyValuePair<string, string>> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      return null;
    });
  }

  public getAllCountries(): any {
    return this.httpClient.get(this.countriesUrl).then(result => {
      const response: DataGenericResponse<KeyValuePair<string, string>> = JSON.parse(result.response);
      return response;
    });
  }
}
