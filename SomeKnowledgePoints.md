## Angular8 Some Knowled Points
### Angular 模板语法
*ngFor  
*ngIf   
插值表达式 {{}}   
属性绑定 []  
事件绑定 ()

### 父子组件传值  
组件与组件之间嵌套通过selector值进行，父组件在模板中使用子组件的selector值作为标记。  
子组件可通过 @Input() propertiy 作为父组件传值容器。  例如：
父组件模板，将父组件的product传递给子组件product
`<app-product-alerts
  [product]="product">
</app-product-alerts>`  
子组件类设置接收属性  
`@Input() product;`  

