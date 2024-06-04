import { Component, OnInit } from '@angular/core';
import { ResearchProjectDto } from '../../model/model';
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css',
  imports: [NavbarComponent],
  standalone: true
})
export class OverviewComponent implements OnInit{
  researchProjects: ResearchProjectDto[] = [];

  constructor(private dataService: DataService, private router: Router) { 

  }

  ngOnInit(): void {
    this.dataService.getAllProjects().subscribe((projects) => {
      this.researchProjects = projects;
    });
  }

  onProjectClick(project: ResearchProjectDto) {
    this.router.navigate(['/project', project.title]);
  }

  navigateToAddProject(): void {
    this.router.navigate(['/add-project']);
  }
}
