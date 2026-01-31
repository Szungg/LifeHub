import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Friendship, CreateFriendshipRequest, UpdateFriendshipRequest } from '../models/friendship.model';

@Injectable({
  providedIn: 'root'
})
export class FriendshipService {
  private apiUrl = 'http://localhost:5000/api/friendships';

  constructor(private http: HttpClient) {}

  getFriendships(): Observable<Friendship[]> {
    return this.http.get<Friendship[]>(this.apiUrl);
  }

  getAcceptedFriends(): Observable<Friendship[]> {
    return this.http.get<Friendship[]>(`${this.apiUrl}/accepted`);
  }

  sendFriendRequest(receiverId: string): Observable<Friendship> {
    return this.http.post<Friendship>(this.apiUrl, { receiverId });
  }

  updateFriendship(id: number, status: number): Observable<Friendship> {
    return this.http.put<Friendship>(`${this.apiUrl}/${id}`, { status });
  }

  deleteFriendship(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
