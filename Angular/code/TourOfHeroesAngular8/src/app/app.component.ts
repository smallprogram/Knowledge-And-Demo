import { Component, Injector } from '@angular/core';
import { PopupService } from './service/popup.service';
import { createCustomElement } from '@angular/elements';
import { PopupComponent } from './component/popup/popup.component';

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

  constructor(injector: Injector, public popup: PopupService){
    // Convert `PopupComponent` to a custom element.
    const PopupElement = createCustomElement(PopupComponent, {injector});
    // Register the custom element with the browser.
    customElements.define('popup-element', PopupElement);
  }
}
