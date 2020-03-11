import { Component, OnInit, Input } from '@angular/core';
import { AdComponent } from '../ad-component';

@Component({
  selector: 'app-hero-profile',
  templateUrl: './hero-profile.component.html',
  styleUrls: ['./hero-profile.component.scss']
})
export class HeroProfileComponent implements AdComponent {
  @Input() data: any;
}
