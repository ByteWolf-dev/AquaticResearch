import { Routes } from '@angular/router';
import { OverviewComponent } from './components/overview/overview.component';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { ProjectDetailComponent } from './components/project-detail/project-detail.component';
import { AddResearchProjectComponent } from './components/add-research-project/add-research-project.component';
import { AddObservationComponent } from './components/add-observation/add-observation.component';
import { Component } from '@angular/core';

export const routes: Routes = [
    {path: '', component: OverviewComponent, canActivate: [AuthGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'overview', component: OverviewComponent, canActivate: [AuthGuard]},
    {path: 'project/:title', component: ProjectDetailComponent, canActivate: [AuthGuard]},
    {path: 'add-project', component: AddResearchProjectComponent, canActivate: [AuthGuard]},
    {path: 'add-observation/:title', component: AddObservationComponent, canActivate: [AuthGuard]}
  ];
