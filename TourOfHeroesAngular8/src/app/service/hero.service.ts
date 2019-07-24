import { Injectable } from '@angular/core';
import { Hero } from '../model/hero';
import { HEROES } from '../model/mock-heroes';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';




@Injectable({
  providedIn: 'root'
})

export class HeroService {

  private heroesUrl = 'api/heroes';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private messagesService: MessageService,
    private httpClient: HttpClient
  ) { }

  //同步返回数据
  private getHeroes_down(): Hero[] {
    return HEROES;
  }
  //通过rxjs返回Observable，实现异步返回数据
  getHeroesAsync(): Observable<Hero[]> {
    //this.log("获取所有Heroes");
    //return of(HEROES);
    return this.httpClient.get<Hero[]>(this.heroesUrl)
      .pipe(
        tap(_ => this.log("获取所有Heroes")),
        catchError(this.handleError<Hero[]>('getHeroesAsync', []))
      );
  }

  getHeroAsync(id: number): Observable<Hero> {
    //this.log(`正在获取id为${id}的英雄数据`);
    //return of(HEROES.find(hero => hero.id === id));
    const url = `${this.heroesUrl}/${id}`;
    return this.httpClient.get<Hero>(url)
      .pipe(
        tap(_ => this.log(`正在获取id为${id}的英雄数据`)),
        catchError(this.handleError<Hero>(`getHeroAsync id=${id}`))
      );
  }


  udpateHero(hero: Hero) {


    return this.httpClient.put(this.heroesUrl, hero, this.httpOptions)
      .pipe(
        tap(_ => this.log(`正在更新 hero id=${hero.id}`)),
        catchError(this.handleError<any>(`updateHero`))
      );
  }


  addHero(hero: Hero) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(this.heroesUrl, hero, this.httpOptions)
      .pipe(
        tap((newHero: Hero) => this.log(`正在添加 hero id=${newHero.id}`)),
        catchError(this.handleError<any>(`addHero`))
      );
  }

  deleteHero(hero: Hero | number) {
    const id = typeof hero === 'number' ? hero : hero.id;
    const url = `${this.heroesUrl}/${id}`;

    return this.httpClient.delete(url, this.httpOptions)
      .pipe(
        tap(_ => this.log(`正在删除 hero id=${id}`)),
        catchError(this.handleError<Hero>(`deleteHero`))
      );
  }


  searchHeroes(term: string): Observable<Hero[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.httpClient.get<Hero[]>(`${this.heroesUrl}/?name=${term}`)
      .pipe(
        tap(_ => this.log(`正在检索 hero name类似${term}`)),
        catchError(this.handleError<Hero[]>(`searchHeroes`))
      );
  }

  getHeroNo404<Data>(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/?id=${id}`;
    return this.httpClient.get<Hero[]>(url)
      .pipe(
        map(heroes => heroes[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? `以获取` : `没找到`;
          this.log(`${outcome} hero id=${id}`);
        }),
        catchError(this.handleError<Hero>(`getHero id=${id}`))
      );
  }



  private log(message: string) {
    this.messagesService.add(`HeroService: ${message}`);
  }


  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} 失败: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
