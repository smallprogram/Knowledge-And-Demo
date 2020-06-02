import { Injectable } from '@angular/core';
import { QuestionBase } from '../class/question-base';
import { DropdownQuestion } from '../class/dropdown-question';
import { TextboxQuestion } from '../class/textbox-question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  getQuestion() {
    let questions: QuestionBase<any>[] = [
      new DropdownQuestion({
        key: 'brave',
        label: 'Bravery Rating',
        options: [
          { key: 'solid', value: 'Solid' },
          { key: 'great', value: 'Great' },
          { key: 'good', value: 'Good' },
          { key: 'unproven', value: 'Unproven' }
        ],
        order: 3
      }),

      new TextboxQuestion({
        key: 'firstName',
        label: 'First Name',
        value: 'Bombasto',
        required: true,
        order: 2
      })
    ];

    return questions.sort((a, b) => a.order - b.order);
  }

  constructor() { }
}
