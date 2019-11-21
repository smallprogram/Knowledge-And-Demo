import { Observable, fromEvent } from 'rxjs';

let okButton = document.getElementById('okButton');

let clicksObservable$ = fromEvent(okButton, 'click');

clicksObservable$.subscribe(
    // process values
    // process error
    // process completion (unnecessary)
);
