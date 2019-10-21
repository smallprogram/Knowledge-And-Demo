import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { ReactiveFormsComponent } from './demo/reactive-forms/reactive-forms.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TemplateDrivenFormsComponent } from './demo/template-driven-forms/template-driven-forms.component';
import { FormValidationComponent } from './demo/form-validation/form-validation.component';
import { DyncmiaFormMainComponent } from './demo/dyncmia-form-main/dyncmia-form-main.component';


const routes: Routes = [

  { path: 'dashboard', component: DashboardComponent },
  { path: '', redirectTo: 'dashboard',pathMatch:'full' },
  { path: 'reactiveforms', component: ReactiveFormsComponent },
  { path: 'templatedriveforms', component: TemplateDrivenFormsComponent },
  { path: 'formvalidation', component: FormValidationComponent},
  { path: 'dyncmiaform', component: DyncmiaFormMainComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
