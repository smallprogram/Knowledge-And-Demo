import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Hero } from 'src/app/class/Hero';


@Component({
  selector: 'app-form-validation',
  templateUrl: './form-validation.component.html',
  styleUrls: ['./form-validation.component.scss']
})
export class FormValidationComponent implements OnInit {

  heroForm:FormGroup;
  powers = ['Really Smart', 'Super Flexible', 'Weather Changer'];
  hero = {name:'zhusir',alterEgo:'zhusir DavidMandy',power:this.powers[0]}

  constructor() { }

  ngOnInit() {
    this.heroForm = new FormGroup({
      'name':new FormControl(this.hero.name,[Validators.required,Validators.minLength(4)]),
      'alterEgo':new FormControl(this.hero.alterEgo),
      'power':new FormControl(this.hero.power,Validators.required)
    });
  }


  get name() { return this.heroForm.get('name'); }

  get power() { return this.heroForm.get('power'); }

  get alterEgo() { return this.heroForm.get('alterEgo'); }

}
