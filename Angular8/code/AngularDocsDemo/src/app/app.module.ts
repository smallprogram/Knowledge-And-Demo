import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { FormsModule }   from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { ReactiveFormsComponent } from './demo/reactive-forms/reactive-forms.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatCardModule } from '@angular/material/card';

import { TemplateDrivenFormsComponent } from './demo/template-driven-forms/template-driven-forms.component';
import { FormValidationComponent } from './demo/form-validation/form-validation.component';
import { ForbiddenValidatorDirective } from './demo/form-validation/customValidators/forbidden-name.directive';
import { IdentityRevealedValidatorDirective } from './demo/form-validation/customValidators/identity-revealed.directive';
import { UniqueAlterEgoValidatorDirective } from './demo/form-validation/customValidators/alter-ego.directive';
import { DynamicFormComponent } from './demo/dynamic-form/dynamic-form.component';
import { DynamicFormQuestionComponent } from './demo/dynamic-form/form-control/dynamic-form-question/dynamic-form-question.component';
import { DyncmiaFormMainComponent } from './demo/dyncmia-form-main/dyncmia-form-main.component';
import { HttpClientComponent } from './demo/http-client/http-client.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    ReactiveFormsComponent,
    DashboardComponent,
    TemplateDrivenFormsComponent,
    FormValidationComponent,
    ForbiddenValidatorDirective,
    IdentityRevealedValidatorDirective,
    UniqueAlterEgoValidatorDirective,
    DynamicFormComponent,
    DynamicFormQuestionComponent,
    DyncmiaFormMainComponent,
    HttpClientComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
