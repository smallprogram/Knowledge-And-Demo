import { Component, OnInit } from '@angular/core';
import { Hero } from 'src/app/model/hero';
import { HEROES } from 'src/app/model/mock-heroes';
import { HeroService } from 'src/app/service/hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.scss']
})
export class HeroesComponent implements OnInit {

  // hero: Hero = {
  //   id: 1,
  //   name: "Windstorm风暴英雄"
  // };

  heroes: Hero[];
  //selectHero: Hero;

  constructor(private heroService: HeroService) {

  }

  ngOnInit() {
    //this.heroes = this.heroService.getHeroes_down(); //同步方式

    //采用rxjs的observable订阅实现异步
    this.heroService.getHeroesAsync().subscribe(heroes => this.heroes = heroes);
  }

  add(name: string) {
    name = name.trim();
    if (!name) {
      return;
    }
    this.heroService.addHero({name} as Hero).subscribe(hero => {
      this.heroes.push(hero);
    });
  }

  delete(hero){
    this.heroes = this.heroes.filter(h => h !== hero);
    this.heroService.deleteHero(hero).subscribe();
  }
  // onSelect(hero) {
  //   this.selectHero = hero;
  // }
}
