import { Component, OnInit, Input, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { AdItem } from '../ad-item';
import { AdDirective } from '../ad.directive';
import { AdComponent } from '../ad-component';

@Component({
  selector: 'app-ad-banner',
  templateUrl: './ad-banner.component.html',
  styleUrls: ['./ad-banner.component.scss']
})
export class AdBannerComponent implements OnInit {

  @Input() ads: AdItem[]; //输入的组件类和组件需要的相关数据
  currentAdIndex = -1;
  @ViewChild(AdDirective, {static: true}) adHost: AdDirective; //使用ViewChild获取AdDirective的实例
  interval: any;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    //首次获取广告
    this.loadComponent();
    //开启计时器并在计时器中继续获取广告
    this.getAds();
  }

  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    //结束时清理计数器
    clearInterval(this.interval);
  }
  

  //选取一个广告的方法
  loadComponent() {
    //currentAdIndex++之后除以广告数组长度的余数作为索引
    this.currentAdIndex = (this.currentAdIndex + 1) % this.ads.length;
    //设置一个变量获取索引处的广告对象
    const adItem = this.ads[this.currentAdIndex];
    //使用组件工厂生成该广告对象的组件对象
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(adItem.component);
    //设置一个变量获取adHost指令的ViewContainerRef容器，并清空该容器
    const viewContainerRef = this.adHost.ViewContainerRef;
    viewContainerRef.clear();
    //使用工厂生成的组件对象指向刚刚获取的AdHost指令中的视图容器并赋值给一个变量
    const componentRef = viewContainerRef.createComponent(componentFactory);
    //最后将广告对象的广告数据赋值给容器中的这个组件
    (<AdComponent>componentRef.instance).data = adItem.data;
    // console.log("1");
  }

  getAds() {
    this.interval = setInterval(() => {
      this.loadComponent();
    }, 3000);
  }
}
