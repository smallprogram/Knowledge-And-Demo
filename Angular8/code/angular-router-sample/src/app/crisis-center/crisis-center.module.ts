import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }    from '@angular/forms';
import { CrisisCenterComponent } from './crisis-center/crisis-center.component';




@NgModule({
  declarations: [

  CrisisCenterComponent],
  imports: [
    CommonModule,
    FormsModule,
  ]
})
export class CrisisCenterModule { }
