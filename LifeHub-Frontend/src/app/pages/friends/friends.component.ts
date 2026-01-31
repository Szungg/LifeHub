import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Friendship {
  id: string;
  requesterId: string;
  receiverId: string;
  status: 'Pending' | 'Accepted' | 'Rejected' | 'Blocked';
  createdAt?: Date;
}

@Component({
  selector: 'app-friends',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit {
  friendships: Friendship[] = [];
  loading = false;

  constructor() {}

  ngOnInit(): void {
    // Load friendships
  }

  acceptRequest(id: string): void {
    console.log('Accept friend request:', id);
  }

  rejectRequest(id: string): void {
    console.log('Reject friend request:', id);
  }

  removeFriend(id: string): void {
    console.log('Remove friend:', id);
  }

  getFriendshipStatusText(status: string): string {
    const statusMap: { [key: string]: string } = {
      'Pending': 'Pendiente',
      'Accepted': 'Aceptado',
      'Rejected': 'Rechazado',
      'Blocked': 'Bloqueado'
    };
    return statusMap[status] || status;
  }
}
