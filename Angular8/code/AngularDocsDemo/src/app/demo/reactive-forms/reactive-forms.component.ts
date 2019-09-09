import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reactive-forms',
  templateUrl: './reactive-forms.component.html',
  styleUrls: ['./reactive-forms.component.scss']
})
export class ReactiveFormsComponent implements OnInit {
  //单个FormControl实例控制单个表单控件
  name = new FormControl('');
  updateName() {
    this.name.setValue("zhusir");
  }


  //FormGroup实例控制多个表单控件
  profileForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    address: new FormGroup({
      street: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      zip: new FormControl('')
    })
  })
  onSubmit() {
    console.warn(this.profileForm.value);
  }
  constructor() { }

  ngOnInit() {
  }



}
