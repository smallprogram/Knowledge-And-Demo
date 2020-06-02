import { Directive, Injectable, forwardRef } from '@angular/core';
import { AsyncValidator, AbstractControl, ValidationErrors, NG_ASYNC_VALIDATORS } from '@angular/forms';
import { HerosService } from './heros.service';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

//响应式表单异步验证器
@Injectable({providedIn:'root'})
export class UniqueAlterEgoValidator implements AsyncValidator{
  constructor(private herosService:HerosService) {}

  validate(ctrl:AbstractControl):
  Promise<ValidationErrors | null> | 
  Observable<ValidationErrors | null>{
    return this.herosService.isAlterEgoTaken(ctrl.value).pipe(
      map(isTaken => (isTaken? {uniqueAlterEgo:true}:null)),
      catchError(() => null)
    );
  }
}

//模板驱动表单异步验证器
@Directive({
  selector: '[appUniqueAlterEgo]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: forwardRef(() => UniqueAlterEgoValidator),
      multi: true
    }
  ]
})
export class UniqueAlterEgoValidatorDirective {
  constructor(private validator: UniqueAlterEgoValidator) {}

  validate(control: AbstractControl) {
    this.validator.validate(control);
  }
}
