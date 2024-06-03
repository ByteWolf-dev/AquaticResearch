import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OverviewComponent } from './components/overview/overview.component';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {path: '', component: OverviewComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'overview', component: OverviewComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
