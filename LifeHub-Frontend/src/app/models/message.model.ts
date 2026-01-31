export interface Message {
  id: number;
  senderId: string;
  receiverId: string;
  content: string;
  sentAt: Date;
  isRead: boolean;
}

export interface CreateMessageRequest {
  receiverId: string;
  content: string;
}
