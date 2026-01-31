import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

interface Recommendation {
  id: string;
  userId: string;
  title: string;
  description: string;
  type: 'Movie' | 'Book' | 'Series';
  genre?: string;
  averageRating?: number;
  ratingCount?: number;
  createdAt?: Date;
}

@Component({
  selector: 'app-recommendations',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.scss']
})
export class RecommendationsComponent implements OnInit {
  recommendations: Recommendation[] = [];
  createForm!: FormGroup;
  loading = false;
  showForm = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      type: ['Movie', Validators.required],
      genre: [''],
      author: [''],
      year: ['']
    });
  }

  onSubmit(): void {
    if (this.createForm.invalid) return;
    console.log('Create recommendation:', this.createForm.value);
    this.createForm.reset();
    this.showForm = false;
  }

  deleteRecommendation(id: string): void {
    console.log('Delete recommendation:', id);
  }

  rateRecommendation(id: string): void {
    console.log('Rate recommendation:', id);
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
  }

  getTypeText(type: string): string {
    const types: { [key: string]: string } = {
      'Movie': 'Película',
      'Book': 'Libro',
      'Series': 'Serie'
    };
    return types[type] || type;
  }
}
