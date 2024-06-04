import { Component, Input, OnInit, input } from '@angular/core';
import { ObservationDto, ResearchProjectDto } from '../../model/model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.css'
})
export class ProjectDetailComponent implements OnInit {
  title: string = '';
  observations: ObservationDto[] = [];

  constructor(private route: ActivatedRoute, private dataService: DataService) { }

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
}
