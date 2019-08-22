import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Tour Of Heroes for Angular8';
  isUnchange = true;
  changeisUnchange()
  {
    this.isUnchange = !this.isUnchange;
  }
}
