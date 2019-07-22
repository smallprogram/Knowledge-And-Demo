import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  items = []; //用于存放购物车商品信息

  constructor(private httpClient: HttpClient) { }

  //添加商品到购物车
  addToCart(product) {
    this.items.push(product);
  }

  //获取购物查商品
  getItems() {
    return this.items;
  }

  //清理购物车
  clearCart() {
    this.items = [];
    return this.items;
  }

  //获取邮寄方式
  getShippingPrices(){
    return this.httpClient.get("./assets/shipping.json");
  }
}
