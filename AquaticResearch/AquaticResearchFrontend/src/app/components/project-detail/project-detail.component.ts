import { Component, Input, OnInit, input } from '@angular/core';
import { ObservationDto, ResearchProjectDto } from '../../model/model';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [NavbarComponent],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.css'
})
export class ProjectDetailComponent implements OnInit {
  title: string = '';
  observations: ObservationDto[] = [];

  constructor(private route: ActivatedRoute, private dataService: DataService, private router : Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.title = params['title'];
    });

    this.dataService
    .getAllObservationsForProject(this.title)
    .subscribe((observations) => {
      this.observations = observations;
    });
  }

  addObservation(title: string): void {
    this.router.navigate(['add-observation/:title', { title }]);
  }
}
