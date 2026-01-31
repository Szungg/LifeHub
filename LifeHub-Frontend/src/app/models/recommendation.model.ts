export interface Recommendation {
  id: number;
  userId: string;
  title: string;
  description: string;
  type: RecommendationType;
  genre?: string;
  author?: string;
  year?: number;
  externalLink?: string;
  coverImageUrl?: string;
  averageRating: number;
  totalRatings: number;
  createdAt: Date;
  user?: any;
}

export enum RecommendationType {
  Movie = 0,
  Book = 1,
  Series = 2
}

export interface CreateRecommendationRequest {
  title: string;
  description: string;
  type: RecommendationType;
  genre?: string;
  author?: string;
  year?: number;
  externalLink?: string;
  coverImageUrl?: string;
}

export interface UpdateRecommendationRequest extends CreateRecommendationRequest {}

export interface RecommendationRatingRequest {
  rating: number;
  comment?: string;
}
