import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../../services/data.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ObservationDto } from '../../model/model';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-add-observation',
  standalone: true,
  imports: [ReactiveFormsModule, NavbarComponent],
  templateUrl: './add-observation.component.html',
  styleUrl: './add-observation.component.css'
})
export class AddObservationComponent implements OnInit {
  projectTitle: string = '';
  observationForm: FormGroup = new FormGroup({});

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private dataService: DataService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectTitle = params['title'];
    });

    this.observationForm = this.fb.group({
      notes: ['', Validators.required],
      observationDateTime: ['', Validators.required],
      researchers: ['', Validators.required],
      equipment: ['', Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.observationForm.valid) {
      const observation: ObservationDto = this.observationForm.value;
      
      observation.location = `${this.observationForm.value.latitude},${this.observationForm.value.longitude}`;
      
      this.dataService.addObservation(observation, this.projectTitle).subscribe(() => {
        this.observationForm.reset();
      });
    }
  }
}
