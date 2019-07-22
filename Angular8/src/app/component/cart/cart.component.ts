import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  items;

  constructor(private cartService: CartService) { 
    this.items = cartService.getItems();
  }

  ngOnInit() {
  }

}
