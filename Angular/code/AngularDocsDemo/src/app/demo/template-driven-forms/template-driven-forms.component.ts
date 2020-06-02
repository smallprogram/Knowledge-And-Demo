import { Component, OnInit } from '@angular/core';
import { Hero } from 'src/app/class/Hero';

@Component({
  selector: 'app-template-driven-forms',
  templateUrl: './template-driven-forms.component.html',
  styleUrls: ['./template-driven-forms.component.scss']
})
export class TemplateDrivenFormsComponent implements OnInit {


  powers = ['不死','不怕冷','不用吃饭','不用睡觉'];

  model = new Hero(18,'zhusir',this.powers[0],'迟到不挨罚');

  submitted = false;

  onSubmit(){
    this.submitted = true;
  }

  // TODO: Remove this when we're done
  get diagnostic() { return JSON.stringify(this.model); }

  newHero(){
    this.model = new Hero(42,'','');
  }

  
  constructor() { }

  ngOnInit() {
  }

}

