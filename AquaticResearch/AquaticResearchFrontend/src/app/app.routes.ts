import { Routes } from '@angular/router';
import { OverviewComponent } from './components/overview/overview.component';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { ProjectDetailComponent } from './components/project-detail/project-detail.component';

export const routes: Routes = [
    {path: '', component: OverviewComponent, canActivate: [AuthGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'overview', component: OverviewComponent, canActivate: [AuthGuard]},
    {path: 'project/:title', component: ProjectDetailComponent, canActivate: [AuthGuard]}
  ];
