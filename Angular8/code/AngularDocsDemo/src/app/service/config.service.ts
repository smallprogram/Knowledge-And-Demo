import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Config } from '../interface/config';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient) { }

  configUrl = 'assets/config.json';
  //#region 请求带类型的响应
  getConfig() {
    return this.http.get(this.configUrl);
  }

  getConfigv2() {
    return this.http.get<Config>(this.configUrl);
  }
  //#endregion

  //#region 读取完整响应体
  getConfigResponse(): Observable<HttpResponse<Config>> {
    return this.http.get<Config>(
      this.configUrl, { observe: 'response' });
  }
  //#endregion

}
