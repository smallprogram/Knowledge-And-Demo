import { Injectable } from '@angular/core';
import { Hero } from '../model/hero';
import { HEROES } from '../model/mock-heroes';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  constructor() { }

  getHeroes(): Hero[] {
    return HEROES;
  }
}
