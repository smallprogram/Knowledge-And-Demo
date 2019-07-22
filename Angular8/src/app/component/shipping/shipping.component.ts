import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-shipping',
  templateUrl: './shipping.component.html',
  styleUrls: ['./shipping.component.scss']
})
export class ShippingComponent implements OnInit {

  shippingCosts;

  constructor(private cartService: CartService) { 
    this.shippingCosts = cartService.getShippingPrices();
  }

  ngOnInit() {
  }

}
