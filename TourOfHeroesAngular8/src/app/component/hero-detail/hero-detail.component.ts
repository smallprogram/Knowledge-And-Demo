import { Component, OnInit, Input } from '@angular/core';
import { Hero } from 'src/app/model/hero';
import { ActivatedRoute } from '@angular/router';
import { HeroService } from 'src/app/service/hero.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.scss']
})
export class HeroDetailComponent implements OnInit {

  // @Input() hero: Hero;
  hero: Hero;
  constructor(private route: ActivatedRoute,   //获取路由相关信息的。此处获取路由URI参数之用
              private heroesService: HeroService,
              private location: Location   
    ) { 
    }

  ngOnInit() {
    this.getHero();
  }

  getHero(){
    const id = +this.route.snapshot.paramMap.get('id'); //获取路由URI参数
    this.heroesService.getHeroAsync(id).subscribe(hero => this.hero = hero);
  }

  goBack(){
    this.location.back();
  }

  save(){
    this.heroesService.udpateHero(this.hero).subscribe(()=> this.goBack());
  }
}
