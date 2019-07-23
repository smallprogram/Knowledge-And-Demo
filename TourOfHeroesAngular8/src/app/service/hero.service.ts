import { Injectable } from '@angular/core';
import { Hero } from '../model/hero';
import { HEROES } from '../model/mock-heroes';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  constructor(private messagesService: MessageService) { }

  //同步返回数据
  getHeroes_down(): Hero[] {
    return HEROES;
  }
  //通过rxjs返回Observable，实现异步返回数据
  getHeroesAsync(): Observable<Hero[]> {
    this.messagesService.add("获取所有Heroes");
    return of(HEROES);
  }

  getHeroAsync(id: number): Observable<Hero>{
    this.messagesService.add(`正在获取id为${id}的英雄数据`);
    return of(HEROES.find(hero => hero.id === id));
  }
}
