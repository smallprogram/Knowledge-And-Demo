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
  selectHero: Hero;

  constructor(private heroService: HeroService) {
    
  }

  ngOnInit() {
    this.heroes = this.heroService.getHeroes();
  }

  onSelect(hero) {
    this.selectHero = hero;
  }
}
