export interface User {
  id: string;
  email: string;
  fullName?: string;
  profilePictureUrl?: string;
  bio?: string;
}

export interface AuthResponse {
  success: boolean;
  message?: string;
  token?: string;
  user?: User;
}
