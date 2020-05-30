# [返回主目录](Readme.md)<!-- omit in toc --> 

# 目录 <!-- omit in toc --> 

- [HttpClient](#httpclient)
- [从服务器获取数据](#从服务器获取数据)
  - [Get请求JSON数据，返回Observable对象](#get请求json数据返回observable对象)
  - [Get请求获取完整的JSON响应信息](#get请求获取完整的json响应信息)
  - [JSONP的Get请求](#jsonp的get请求)
  - [Get请求非JSON数据](#get请求非json数据)
- [错误处理](#错误处理)
  - [获取错误详情](#获取错误详情)
  - [错误发生后重试](#错误发生后重试)
- [Http Headers(Http头)](#http-headershttp头)
  - [为请求添加Http Headers](#为请求添加http-headers)


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

## 错误处理
HttpClient返回的响应是一个Observable类型。通过Observable的第二个回调函数处理错误
```ts
showConfig() {
  this.configService.getConfig()
    .subscribe(
      (data: Config) => this.config = { ...data }, // success path
      error => this.error = error // error path
    );
}
```

### 获取错误详情
直接返回原始错误不友好，上例中，如果返回错误，错误的类型是HttpErrorResponse，它包含了一些有用的错误信息。

错误分为两种：
1. 服务端错误
2. 客户端错误


如果服务端返回的错误代码是404或500，它会返回一个错误的响应体。

如果客户端出现了错误，就会抛出一个Error类型的异常。

需要获取错误详情首先需要一个错误处理器
```ts
private handleError(error: HttpErrorResponse) {
  if (error.error instanceof ErrorEvent) {
    // A client-side or network error occurred. Handle it accordingly.
    console.error('An error occurred:', error.error.message);
  } else {
    // The backend returned an unsuccessful response code.
    // The response body may contain clues as to what went wrong,
    console.error(
      `Backend returned code ${error.status}, ` +
      `body was: ${error.error}`);
  }
  // return an observable with a user-facing error message
  return throwError(
    'Something bad happened; please try again later.');
};
```
这个错误处理器会处理来自客户端和服务端的错误。

之后将这个错误处理器通过管道应用到HttpClient请求中。
```ts
getConfig() {
  return this.http.get<Config>(this.configUrl)
    .pipe(
      catchError(this.handleError)
    );
}
```

### 错误发生后重试
有时错误的发生可能是暂时的，例如网络中断等情况。可以通过重试拿到正确的结果。

利用RxJS库retry操作符实现对失败的请求进行重试操作。
```ts
getConfig() {
  return this.http.get<Config>(this.configUrl)
    .pipe(
      retry(3), // retry a failed request up to 3 times
      catchError(this.handleError) // then handle the error
    );
}
```

## Http Headers(Http头)
许多请求需要包好Http Headers，例如Content-Type，声明要返回的资源类型。或者服务端需要一个授权令牌，这些也要写在Http Headers中。

### 为请求添加Http Headers


