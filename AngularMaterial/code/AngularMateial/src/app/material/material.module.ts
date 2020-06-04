import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { MaterialCdkModule } from '../common/material-cdk.module';



import { MaterialRoutingModule } from './material-routing.module';

import { SliderComponent } from './slider/slider.component';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
  declarations: [
    DashboardComponent,
    SliderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialCdkModule,
    MaterialRoutingModule
  ]
})
export class MaterialModule { }
