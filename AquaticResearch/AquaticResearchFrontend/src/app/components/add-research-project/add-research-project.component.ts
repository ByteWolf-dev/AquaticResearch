import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { ResearchProjectDto } from '../../model/model';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-add-research-project',
  standalone: true,
  imports: [ReactiveFormsModule, NavbarComponent],
  templateUrl: './add-research-project.component.html',
  styleUrl: './add-research-project.component.css'
})
export class AddResearchProjectComponent implements OnInit{
  researchProjectForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private dataService: DataService) {}

  ngOnInit(): void {
    this.researchProjectForm = this.fb.group({
      title: ['', Validators.required],
      species: this.fb.group({
        name: ['', Validators.required],
        scientificName: ['', Validators.required]
      })
    });
  }

  onSubmit(): void {
    if (this.researchProjectForm.valid) {
      const researchProject: ResearchProjectDto = this.researchProjectForm.value;
      this.dataService.addProject(researchProject).subscribe(() => {
        this.researchProjectForm.reset();
      });
    }
  }
}
