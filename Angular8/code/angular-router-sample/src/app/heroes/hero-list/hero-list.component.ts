import { Component, OnInit } from '@angular/core';
import { Hero } from '../class/hero';
import { HeroService } from '../service/hero.service';
import { MessageService } from 'src/app/service/message.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-hero-list',
  templateUrl: './hero-list.component.html',
  styleUrls: ['./hero-list.component.scss']
})
export class HeroListComponent implements OnInit {

  heroes$: Observable<Hero[]>;
  selectedId: number;

  constructor(
    private heroService: HeroService,
    private messageService: MessageService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.heroes$ = this.route.paramMap.pipe(
      switchMap(params => {
        this.selectedId =+ params.get('id');  //=+会把后边的参数转换为数字赋值给前面的参数
        return this.heroService.getHeroes();
      })
    )
  }

  onSelect(hero: Hero): void {
    this.messageService.add(`HeroService: Selected hero id=${hero.id}`);
  }


}
