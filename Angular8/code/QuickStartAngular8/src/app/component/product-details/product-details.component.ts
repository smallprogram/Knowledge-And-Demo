import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { products } from 'src/app/model/products';
import { CartService } from 'src/app/service/cart.service';




@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product;

  constructor(private route: ActivatedRoute, private cartService: CartService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.product = products[+params.get('productId')];  //从URI中获取productId
    })
  }

  addToCart(product)
  {
    this.cartService.addToCart(product);
    window.alert("商品成功添加到购物车");
  }

}
