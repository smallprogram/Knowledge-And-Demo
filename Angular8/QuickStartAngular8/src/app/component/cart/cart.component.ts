import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { CartService } from 'src/app/service/cart.service';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  items; //购物车内的物品
  checkoutForm; //表单模型

  constructor(private cartService: CartService,
    private formBuilder: FormBuilder) {
    this.items = cartService.getItems();

    this.checkoutForm = this.formBuilder.group({
      name:'',
      address:''
    });

  }

  onSubmit(costomerData){
    console.warn('你的订单已经提交',costomerData);
    window.alert('你的订单已经提交,信息在控制台可以看到');

    this.items = this.cartService.clearCart();
    this.checkoutForm.reset();
  }

  ngOnInit() {
  }

}
