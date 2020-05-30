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
### JSONP的Get请求
当服务端不支持CORS协议时(不支持跨域请求时)，可以使用HttpClient发出跨域的JSONP请求。之后返回一个Observable。订阅使用或使用async管道管理结果之前使用RxJS map运算符转换响应。
```ts
/* GET heroes whose name contains search term */
searchHeroes(term: string): Observable {
  term = term.trim();

  let heroesURL = `${this.heroesURL}?${term}`;
  return this.http.jsonp(heroesUrl, 'callback').pipe(
      catchError(this.handleError('searchHeroes', []) // then handle the error
    );
};
```

### Get请求非JSON数据
从服务器读取文本文件， 并把文件的内容记录下来，然后把这些内容使用 `Observable<string>` 的形式返回给调用者。
```ts
getTextFile(filename: string) {
  // The Observable returned by get() is of type Observable<string>
  // because a text response was specified.
  // There's no need to pass a <string> type parameter to get().
  return this.http.get(filename, {responseType: 'text'})
    .pipe(
      tap( // Log the result or error
        data => this.log(filename, data),
        error => this.logError(filename, error)
      )
    );
}
```
```ts
download() {
  this.downloaderService.getTextFile('assets/textfile.txt')
    .subscribe(results => this.contents = results);
}
```


