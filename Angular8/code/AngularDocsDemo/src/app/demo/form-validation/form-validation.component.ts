import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Hero } from 'src/app/class/Hero';
import { forbiddenNameValidator } from './customValidators/forbidden-name.directive';
import { identityRevealedValidator } from './customValidators/identity-revealed.directive';
import { UniqueAlterEgoValidator } from './customValidators/alter-ego.directive';


@Component({
  selector: 'app-form-validation',
  templateUrl: './form-validation.component.html',
  styleUrls: ['./form-validation.component.scss']
})
export class FormValidationComponent implements OnInit {

  //#region 响应式表单验证
  heroForm: FormGroup;
  powers = ['Really Smart', 'Super Flexible', 'Weather Changer'];
  hero = { name: 'zhusir', alterEgo: 'zhusir DavidMandy', power: this.powers[0] }

  constructor(private alterEgoValidator: UniqueAlterEgoValidator) { }

  ngOnInit() {
    this.heroForm = new FormGroup({
      'name': new FormControl(this.hero.name, [
        Validators.required,
        Validators.minLength(4),
        forbiddenNameValidator(/bob/i)
      ]),
      'alterEgo': new FormControl(this.hero.alterEgo, {
        asyncValidators: [this.alterEgoValidator.validate.bind(this.alterEgoValidator)],
        updateOn:'blur'
      }),
      'power': new FormControl(this.hero.power, Validators.required)
    }, { validators: identityRevealedValidator });
  }


  get name() { return this.heroForm.get('name'); }

  get power() { return this.heroForm.get('power'); }

  get alterEgo() { return this.heroForm.get('alterEgo'); }

  //#endregion 响应式表单验证

  //#region 模板驱动表单验证
  powers1 = ['Really Smart', 'Super Flexible', 'Weather Changer'];

  hero1 = {name1: 'Dr.', alterEgo1: 'Dr. What', power1: this.powers1[0]};
  //#endregion 模板驱动表单验证

}
