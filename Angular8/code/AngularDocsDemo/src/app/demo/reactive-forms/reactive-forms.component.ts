import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';

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
    firstName: new FormControl('',Validators.required),
    lastName: new FormControl('',[Validators.required,Validators.minLength(4)]),
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
  updateProfile() {
    this.profileForm.patchValue({
      firstName: "zhusir",
      address: {
        street: "united statued"
      }
    });
  }

  //使用FormBuilder创建表单
  constructor(private fb: FormBuilder) { }

  fbProfileForm = this.fb.group({
    firstName1:['',Validators.required],
    lastName1:[''],
    address1:this.fb.group({
      street1: [''],
      city1: [''],
      state1: [''],
      zip1: ['']
    }),
    aliases:this.fb.array([
      this.fb.control('')
    ])
  });

  get aliases(){
    return this.fbProfileForm.get('aliases') as FormArray;
  }

  addAlias(){
    this.aliases.push(this.fb.control(''));
  }


  ngOnInit() {
  }



}
