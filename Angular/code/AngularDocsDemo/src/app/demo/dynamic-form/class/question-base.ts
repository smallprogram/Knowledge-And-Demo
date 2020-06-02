export class QuestionBase<T> {
    value: T;  //表单控件的值
    key: string;  
    label: string;
    required: boolean;  //是否必填
    order: number;      //顺序
    controlType: string; //表单控件类型
    type: string;
    options: {key: string, value: string}[];
  
    constructor(options: {
        value?: T,
        key?: string,
        label?: string,
        required?: boolean,
        order?: number,
        controlType?: string
        type?: string
      } = {}) {
      this.value = options.value;
      this.key = options.key || '';
      this.label = options.label || '';
      this.required = !!options.required;
      this.order = options.order === undefined ? 1 : options.order;
      this.controlType = options.controlType || '';
      this.type = options.type || '';
    }
  }