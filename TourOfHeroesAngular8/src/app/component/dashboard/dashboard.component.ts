import { Component, OnInit } from '@angular/core';
import { Hero } from 'src/app/model/hero';
import { HeroService } from 'src/app/service/hero.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  heroes: Hero[] = [];
  constructor(private heroesService: HeroService) { }

  ngOnInit() {
    this.getHeroes();
  }

  getHeroes(){
    this.heroesService.getHeroesAsync().subscribe(heroes => this.heroes = heroes.slice(1,5));
  }

}
