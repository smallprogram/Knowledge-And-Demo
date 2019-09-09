import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { ReactiveFormsComponent } from './demo/reactive-forms/reactive-forms.component';
import { DashboardComponent } from './dashboard/dashboard.component';


const routes: Routes = [

  { path: 'dashboard', component: DashboardComponent },
  { path: '', redirectTo: 'dashboard',pathMatch:'full' },
  { path: 'reactiveforms', component: ReactiveFormsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
