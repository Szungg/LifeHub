import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MusicFile, CreateMusicFileRequest, UpdateMusicFileRequest } from '../models/musicfile.model';

@Injectable({
  providedIn: 'root'
})
export class MusicFileService {
  private apiUrl = 'http://localhost:5000/api/musicfiles';

  constructor(private http: HttpClient) {}

  getMusicFiles(): Observable<MusicFile[]> {
    return this.http.get<MusicFile[]>(this.apiUrl);
  }

  getMusicFile(id: number): Observable<MusicFile> {
    return this.http.get<MusicFile>(`${this.apiUrl}/${id}`);
  }

  createMusicFile(data: CreateMusicFileRequest): Observable<MusicFile> {
    return this.http.post<MusicFile>(this.apiUrl, data);
  }

  updateMusicFile(id: number, data: UpdateMusicFileRequest): Observable<MusicFile> {
    return this.http.put<MusicFile>(`${this.apiUrl}/${id}`, data);
  }

  deleteMusicFile(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
