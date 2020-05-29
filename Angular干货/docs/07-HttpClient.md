# [返回主目录](Readme.md)<!-- omit in toc --> 

# 目录 <!-- omit in toc --> 

## HttpClient
基于XMLHttpRequest接口。包含在HttpClientModule库中，路径为@angular/common/http。

使用时需要在NgModeule中导入HttpClientModule。并通过依赖注入HttpClient到需要的服务或组件中。


## 从服务器获取数据

### Get请求JSON数据，返回Observable对象
```ts
// server side jsondata
{
  "heroesUrl": "api/heroes",
  "textfile": "assets/textfile.txt"
}

// client side service get method
configUrl = 'assets/config.json';

getConfig() {
  return this.http.get(this.configUrl);
}

// client side get response body
config: Config;

showConfig() {
  this.configService.getConfig()
    .subscribe((data: Config) => this.config = {
        heroesUrl: data['heroesUrl'],
        textfile:  data['textfile']
        // heroesUrl: data.heroesUrl,
        // textfile:  data.textfile
    });
}
// dto
export interface Config {
  heroesUrl: string;
  textfile: string;
}
```

### Get请求获取完整的JSON响应信息
```ts
// client side service get full Respose method
getConfigResponse(): Observable<HttpResponse<Config>> {
  return this.http.get<Config>(
    this.configUrl, { observe: 'response' });
}

// client side get fll Response
showConfigResponse() {
  this.configService.getConfigResponse()
    // resp is of type `HttpResponse<Config>`
    .subscribe(resp => {
      // display its headers
      const keys = resp.headers.keys();
      this.headers = keys.map(key =>
        `${key}: ${resp.headers.get(key)}`);

      // access the body directly, which is typed as `Config`.
      this.config = { ... resp.body };
    });
}
```