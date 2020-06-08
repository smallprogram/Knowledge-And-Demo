import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NavComponent } from './nav/nav.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TableComponent } from './table/table.component';
import { TreeComponent } from './tree/tree.component';
import { DragDropComponent } from './drag-drop/drag-drop.component';


const routes: Routes = [
  {
    path: 'material',
    component: NavComponent,
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
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
      },
      { path: 'table', component: TableComponent },
      { path: 'tree', component: TreeComponent },
      { path: 'drag-drop', component: DragDropComponent },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialRoutingModule { }
