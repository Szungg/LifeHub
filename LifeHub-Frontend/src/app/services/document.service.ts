import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Document, CreateDocumentRequest, UpdateDocumentRequest } from '../models/document.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private apiUrl = 'http://localhost:5000/api/documents';

  constructor(private http: HttpClient) {}

  getDocuments(): Observable<Document[]> {
    return this.http.get<Document[]>(this.apiUrl);
  }

  getDocument(id: number): Observable<Document> {
    return this.http.get<Document>(`${this.apiUrl}/${id}`);
  }

  createDocument(data: CreateDocumentRequest): Observable<Document> {
    return this.http.post<Document>(this.apiUrl, data);
  }

  updateDocument(id: number, data: UpdateDocumentRequest): Observable<Document> {
    return this.http.put<Document>(`${this.apiUrl}/${id}`, data);
  }

  deleteDocument(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
