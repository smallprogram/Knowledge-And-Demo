import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SliderComponent } from './slider/slider.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: 'slider',
        component: SliderComponent,
        // children: [
        //   {
        //     path: ':id',
        //     component: CrisisDetailComponent,
        //     canDeactivate: [CanDeactivateGuard],
        //     resolve:{
        //       crisis:CrisisDetailResolverService
        //     }
        //   },
        //   {
        //     path: '',
        //     component: CrisisCenterHomeComponent
        //   }
        // ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialRoutingModule { }
