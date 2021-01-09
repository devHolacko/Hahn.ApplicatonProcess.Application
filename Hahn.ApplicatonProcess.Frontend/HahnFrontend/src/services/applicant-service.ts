import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import * as environment from "../../config/environment.json";

@inject(HttpClient)
export class ApplicantService {
  private httpClient: HttpClient;
  private readonly applicantsUrl: string;
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.applicantsUrl = `${environment.apiUrl}/${environment.applicants}`;
  }

  public getApplicant(id: number): void {
    const requestUrl = `${this.applicantsUrl}/${id}`;
    this.httpClient.get(requestUrl).then(result => {
      console.log({ result });
    });
  }
}
