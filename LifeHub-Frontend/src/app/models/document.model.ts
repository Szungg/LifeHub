export interface Document {
  id: number;
  userId: string;
  title: string;
  description: string;
  content: string;
  type: DocumentType;
  createdAt: Date;
  updatedAt: Date;
}

export enum DocumentType {
  Note = 0,
  TextFile = 1,
  List = 2
}

export interface CreateDocumentRequest {
  title: string;
  description: string;
  content: string;
  type: DocumentType;
}

export interface UpdateDocumentRequest {
  title: string;
  description: string;
  content: string;
}
