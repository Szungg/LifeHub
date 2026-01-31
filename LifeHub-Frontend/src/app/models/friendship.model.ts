export interface Friendship {
  id: number;
  requesterId: string;
  receiverId: string;
  status: FriendshipStatus;
  createdAt: Date;
  requester?: any;
  receiver?: any;
}

export enum FriendshipStatus {
  Pending = 0,
  Accepted = 1,
  Rejected = 2,
  Blocked = 3
}

export interface CreateFriendshipRequest {
  receiverId: string;
}

export interface UpdateFriendshipRequest {
  status: FriendshipStatus;
}
