import { ValidatorFn, AbstractControl, NG_VALIDATORS, Validator } from '@angular/forms';
import { Directive, Input } from '@angular/core';



//自定义同步验证其函数，用于响应式表单
//传入一个正则表达式
export function forbiddenNameValidator(nameRe: RegExp): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const forbidden = nameRe.test(control.value);
        return forbidden ? { 'forbiddenName': { value: control.value } } : null;
    };
}


//用于模板驱动表单的自定义验证器
@Directive({
    selector: '[appForbiddenName]',
    providers: [{ provide: NG_VALIDATORS, useExisting: ForbiddenValidatorDirective, multi: true }]
})

export class ForbiddenValidatorDirective implements Validator {
    @Input('appForbiddenName') forbiddenName: string;
    validate(control: AbstractControl): { [key: string]: any } | null {
        return this.forbiddenName ?
            forbiddenNameValidator(new RegExp(this.forbiddenName, 'i'))(control) : null;
    }
}