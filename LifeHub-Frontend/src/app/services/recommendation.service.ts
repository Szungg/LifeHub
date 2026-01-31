import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recommendation, CreateRecommendationRequest, UpdateRecommendationRequest, RecommendationRatingRequest } from '../models/recommendation.model';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {
  private apiUrl = 'http://localhost:5000/api/recommendations';

  constructor(private http: HttpClient) {}

  getRecommendations(): Observable<Recommendation[]> {
    return this.http.get<Recommendation[]>(this.apiUrl);
  }

  getRecommendation(id: number): Observable<Recommendation> {
    return this.http.get<Recommendation>(`${this.apiUrl}/${id}`);
  }

  getUserRecommendations(userId: string): Observable<Recommendation[]> {
    return this.http.get<Recommendation[]>(`${this.apiUrl}/user/${userId}`);
  }

  createRecommendation(data: CreateRecommendationRequest): Observable<Recommendation> {
    return this.http.post<Recommendation>(this.apiUrl, data);
  }

  updateRecommendation(id: number, data: UpdateRecommendationRequest): Observable<Recommendation> {
    return this.http.put<Recommendation>(`${this.apiUrl}/${id}`, data);
  }

  deleteRecommendation(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  rateRecommendation(id: number, rating: RecommendationRatingRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/rate`, rating);
  }
}
