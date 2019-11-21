import { Observable, from } from 'rxjs';

let booksPromise = getBookByIdAsync(5);

let booksObservable$ = from(booksPromise);

booksObservable$.subscribe(
    // process values
    // process error
    // process completion
);

function getBookByIdAsync(id:any) {
    return new Promise((ful,reject) => {})
}

