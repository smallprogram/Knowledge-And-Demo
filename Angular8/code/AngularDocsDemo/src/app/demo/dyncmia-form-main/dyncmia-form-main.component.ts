import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../dynamic-form/service/question.service';

@Component({
  selector: 'app-dyncmia-form-main',
  templateUrl: './dyncmia-form-main.component.html',
  styleUrls: ['./dyncmia-form-main.component.scss']
})
export class DyncmiaFormMainComponent implements OnInit {
  questions: any[]
  constructor(private service: QuestionService) {
    this.questions = service.getQuestion();
  }

  ngOnInit() {
  }

}
