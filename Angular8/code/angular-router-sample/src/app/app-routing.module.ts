import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ComposeMessageComponent } from './compose-message/compose-message.component';
import { AuthGuard } from './auth/auth.guard';
// import { HeroListComponent } from './heroes/hero-list/hero-list.component';


const routes: Routes = [
  // { path: 'crisis-center', component: CrisisListComponent },
  // { path: 'heroes', component: HeroListComponent },
  // { path: '', component: HeroListComponent },
  {
    path: 'compose',
    component: ComposeMessageComponent,
    outlet: 'popup'
  },
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'crisis-center',
    loadChildren: () => import('./crisis-center/crisis-center.module').then(m => m.CrisisCenterModule),
    data: { preload: true }
  },

  { path: '', redirectTo: '/heroes', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
