import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import * as environment from "../../config/environment.json";
import { DataGenericResponse } from '../models/general/data-generic-response';
import { GenericResponse } from '../models/general/generic-response';
import { GetApplicantByIdResponse } from '../models/applicants/response/get-applicant-by-id';
import { Promise } from '../../node_modules/promise-polyfill/src';
import { CreateApplicantRequest } from '../models/applicants/request/create-applicant';
import { EditApplicantRequest } from '../models/applicants/request/edit-applicant';
import { KeyValuePair } from '../models/general/key-value-pair';
import { ApplicantListItem } from '../models/applicants/response/applicant-list-item';

@inject(HttpClient)
export class ApplicantService {
  private httpClient: HttpClient;
  private readonly applicantsUrl: string;
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.applicantsUrl = `${environment.apiUrl}/${environment.applicants}`;
  }

  public getApplicant(id: number): Promise<DataGenericResponse<GetApplicantByIdResponse>> {
    const requestUrl = `${this.applicantsUrl}/${id}`;
    return this.httpClient.get(requestUrl).then(result => {
      const response: DataGenericResponse<GetApplicantByIdResponse> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      return null;
    });
  }

  public createApplicant(request: CreateApplicantRequest):
    Promise<DataGenericResponse<number>> | Promise<DataGenericResponse<KeyValuePair<string, string>[]>> {
    return this.httpClient.post(this.applicantsUrl, request).then(result => {
      const response: DataGenericResponse<number> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      const response: DataGenericResponse<KeyValuePair<string, string>[]> = JSON.parse(err.response);
      return response;
    });
  }

  public editApplicant(request: EditApplicantRequest):
    Promise<DataGenericResponse<number>> | Promise<DataGenericResponse<KeyValuePair<string, string>[]>> {
    return this.httpClient.put(this.applicantsUrl, request).then(result => {
      const response: DataGenericResponse<number> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      const response: DataGenericResponse<KeyValuePair<string, string>[]> = JSON.parse(err.response);
      return response;
    });
  }

  public deleteApplicant(id: number): Promise<GenericResponse> {
    const requestUrl = `${this.applicantsUrl}/${id}`;
    this.httpClient.delete(requestUrl).then(result => {
      const response: GenericResponse = JSON.parse(result.response);
      return response;
    }).catch(err => {
      const response: GenericResponse = JSON.parse(err.response);
      return response;
    });
  }

  public getApplicantsList()
    : Promise<DataGenericResponse<ApplicantListItem>> | GenericResponse {
    const requestUrl = `${this.applicantsUrl}/list`;
    return this.httpClient.get(requestUrl).then(result => {
      const response: DataGenericResponse<ApplicantListItem[]> = JSON.parse(result.response);
      return response;
    }).catch(err => {
      const response: GenericResponse = JSON.parse(err.response);
      return response;
    });
  }
}
