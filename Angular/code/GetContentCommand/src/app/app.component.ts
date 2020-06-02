import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'GetContentCommand';
  accesslog: any;
  errorlog: any;

  constructor(private http: HttpClient) {
    this.http.get('assets/test.log', { responseType: 'text' }).subscribe(data => {
      this.accesslog = data;
    })

  }



}
