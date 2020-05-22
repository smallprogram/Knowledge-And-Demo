import { Component, OnInit } from '@angular/core';
import { ConfigService } from 'src/app/service/config.service';
import { Config } from 'src/app/interface/config';

@Component({
  selector: 'app-http-client',
  templateUrl: './http-client.component.html',
  styleUrls: ['./http-client.component.scss']
})
export class HttpClientComponent implements OnInit {

  constructor(private configService: ConfigService) { }

  ngOnInit(): void {
  }

  //#region 请求带类型的响应
  configv1: Config;
  configv2: Config;

  showConfigv1() {
    this.configService.getConfig()
      .subscribe((data) => this.configv1 = {
        heroesUrl: data['heroesUrl'],
        textfile: data['textfile']
      });
  }

  showConfigv2() {
    this.configService.getConfigv2()
      .subscribe((data: Config) => this.configv2 = {
        heroesUrl: data.heroesUrl,
        textfile: data.textfile
      });
  }
  //#endregion

  //#region 读取完整响应体
  configv3: Config;
  headers
  showConfigResponse() {
    this.configService.getConfigResponse()
      // resp is of type `HttpResponse<Config>`
      .subscribe(resp => {
        // display its headers
        const keys = resp.headers.keys();
        this.headers = keys.map(key =>
          `${key}: ${resp.headers.get(key)}`);
  
        // access the body directly, which is typed as `Config`.
        this.configv3 = { ... resp.body };
      });
  }

  //#endregion

}
