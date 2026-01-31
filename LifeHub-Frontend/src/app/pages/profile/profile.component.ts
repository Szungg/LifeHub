import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

interface User {
  id: string;
  email: string;
  fullName: string;
  bio?: string;
  profilePictureUrl?: string;
}

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;
  passwordForm!: FormGroup;
  user: User | null = null;
  loading = false;
  success = '';
  error = '';

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initForms();
  }

  initForms(): void {
    this.profileForm = this.fb.group({
      fullName: ['', Validators.required],
      bio: [''],
      profilePictureUrl: ['']
    });

    this.passwordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });
  }

  onProfileSubmit(): void {
    if (this.profileForm.invalid) return;
    console.log('Profile update:', this.profileForm.value);
  }

  onPasswordSubmit(): void {
    if (this.passwordForm.invalid) return;
    console.log('Password change:', this.passwordForm.value);
  }
}
