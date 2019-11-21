import { Observable } from "rxjs";
import { ajax } from "rxjs/ajax";

let httpObservable$ = ajax.getJSON('/api/books');

httpObservable$.subscribe(
    // process values
    // process error
    // process completion
);