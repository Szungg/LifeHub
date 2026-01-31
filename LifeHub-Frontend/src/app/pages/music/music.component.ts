import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

interface MusicFile {
  id: string;
  userId: string;
  fileName: string;
  title: string;
  artist?: string;
  album?: string;
  duration?: number;
  genre?: string;
  localPath?: string;
  createdAt?: Date;
}

@Component({
  selector: 'app-music',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './music.component.html',
  styleUrls: ['./music.component.scss']
})
export class MusicComponent implements OnInit {
  musicFiles: MusicFile[] = [];
  createForm!: FormGroup;
  loading = false;
  showForm = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      fileName: ['', Validators.required],
      title: ['', Validators.required],
      artist: [''],
      album: [''],
      durationSeconds: [''],
      genre: [''],
      localPath: ['']
    });
  }

  onSubmit(): void {
    if (this.createForm.invalid) return;
    console.log('Create music file:', this.createForm.value);
    this.createForm.reset();
    this.showForm = false;
  }

  deleteMusicFile(id: string): void {
    console.log('Delete music file:', id);
  }

  playMusicFile(file: MusicFile): void {
    console.log('Play music file:', file.title);
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
  }

  formatDuration(seconds?: number): string {
    if (!seconds) return 'N/A';
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
  }
}
