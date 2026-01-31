import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

interface Document {
  id: string;
  title: string;
  description?: string;
  content?: string;
  type?: string;
  createdAt?: Date;
  updatedAt?: Date;
}

@Component({
  selector: 'app-documents',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss']
})
export class DocumentsComponent implements OnInit {
  documents: Document[] = [];
  createForm!: FormGroup;
  editForm!: FormGroup;
  loading = false;
  showForm = false;
  selectedDocument: Document | null = null;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      content: [''],
      type: ['Note']
    });

    this.editForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      content: ['']
    });
  }

  onCreate(): void {
    if (this.createForm.invalid) return;
    console.log('Create document:', this.createForm.value);
    this.createForm.reset();
    this.showForm = false;
  }

  onEdit(): void {
    if (!this.selectedDocument || this.editForm.invalid) return;
    console.log('Edit document:', this.editForm.value);
  }

  selectDocument(doc: Document): void {
    this.selectedDocument = doc;
    this.editForm.patchValue(doc);
  }

  deleteDocument(id: string): void {
    console.log('Delete document:', id);
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
  }

  closeForm(): void {
    this.showForm = false;
    this.selectedDocument = null;
  }

  getTypeText(type?: string): string {
    const typeMap: { [key: string]: string } = {
      'Note': 'Nota',
      'TextFile': 'Archivo de Texto',
      'List': 'Lista'
    };
    return type ? (typeMap[type] || type) : 'Nota';
  }
}
