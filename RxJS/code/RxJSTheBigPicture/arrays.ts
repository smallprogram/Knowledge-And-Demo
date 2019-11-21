import { Observable, from } from 'rxjs';

let bookArray = [
    { "bookID": 1, "title": "西游记" },
    { "bookID": 2, "title": "水浒传" },
    { "bookID": 3, "title": "三国演义" }
];

let booksObservable$ = from(bookArray);

booksObservable$.subscribe(
    // process values
    // process error
    // process completion
);