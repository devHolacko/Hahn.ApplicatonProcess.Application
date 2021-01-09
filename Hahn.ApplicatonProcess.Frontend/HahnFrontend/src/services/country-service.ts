import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import * as environment from "../../config/environment.json";
import { DataGenericResponse } from '../models/general/data-generic-response';
import { GenericResponse } from '../models/general/generic-response';
import { GetApplicantByIdResponse } from '../models/applicants/response/get-applicant-by-id';
import { Promise } from '../../node_modules/promise-polyfill/src';
import { CreateApplicant } from '../models/applicants/request/create-applicant';
import { EditApplicant } from '../models/applicants/request/edit-applicant';
import { KeyValuePair } from '../models/general/key-value-pair';
import { ApplicantListItem } from '../models/applicants/response/applicant-list-item';

@inject(HttpClient)
export class CountryService {
  private httpClient: HttpClient;
  private readonly countriesUrl: string;
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.countriesUrl = `${environment.apiUrl}/${environment.countries}`;
  }

  public getCountries(): Promise<DataGenericResponse<KeyValuePair<string, string>>> {
    return this.httpClient.get(this.countriesUrl).then(result => {
      const response: DataGenericResponse<KeyValuePair<string, string>> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      return null;
    });
  }
}
