import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { Message, CreateMessageRequest } from '../models/message.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'http://localhost:5000/api/messages';
  private hubUrl = 'http://localhost:5000/hubs/chat';
  
  private hubConnection: HubConnection | null = null;
  private messageSubject = new BehaviorSubject<Message | null>(null);
  private connectedSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  getConversation(otherUserId: string): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/conversation/${otherUserId}`);
  }

  sendMessage(message: CreateMessageRequest): Observable<Message> {
    return this.http.post<Message>(this.apiUrl, message);
  }

  markAsRead(messageId: number): Observable<Message> {
    return this.http.put<Message>(`${this.apiUrl}/${messageId}/mark-read`, {});
  }

  getUnreadMessages(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/unread`);
  }

  // SignalR Real-time Chat
  startConnection(token: string): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('Connected to chat hub');
        this.connectedSubject.next(true);
      })
      .catch(err => console.log('Connection failed: ', err));

    this.hubConnection.on('ReceiveMessage', (message: Message) => {
      this.messageSubject.next(message);
    });

    this.hubConnection.on('MessageSent', (messageId: number) => {
      console.log('Message sent with ID:', messageId);
    });

    this.hubConnection.on('MessageRead', (messageId: number) => {
      console.log('Message read:', messageId);
    });

    this.hubConnection.on('Error', (error: string) => {
      console.error('Chat error:', error);
    });
  }

  sendMessageRealtime(receiverId: string, content: string): void {
    if (this.hubConnection && this.hubConnection.state === HubConnectionState.Connected) {
      this.hubConnection.invoke('SendMessageAsync', receiverId, content)
        .catch(err => console.error('Error sending message: ', err));
    }
  }

  markMessageAsReadRealtime(messageId: number): void {
    if (this.hubConnection && this.hubConnection.state === HubConnectionState.Connected) {
      this.hubConnection.invoke('MarkMessageAsReadAsync', messageId)
        .catch(err => console.error('Error marking message as read: ', err));
    }
  }

  getMessageUpdates(): Observable<Message | null> {
    return this.messageSubject.asObservable();
  }

  isConnected(): Observable<boolean> {
    return this.connectedSubject.asObservable();
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
      this.connectedSubject.next(false);
    }
  }
}
