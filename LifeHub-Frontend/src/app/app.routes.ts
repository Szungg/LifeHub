import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./pages/auth/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'profile',
    loadComponent: () => import('./pages/profile/profile.component').then(m => m.ProfileComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'friends',
    loadComponent: () => import('./pages/friends/friends.component').then(m => m.FriendsComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'chat',
    loadComponent: () => import('./pages/chat/chat.component').then(m => m.ChatComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'recommendations',
    loadComponent: () => import('./pages/recommendations/recommendations.component').then(m => m.RecommendationsComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'documents',
    loadComponent: () => import('./pages/documents/documents.component').then(m => m.DocumentsComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'music',
    loadComponent: () => import('./pages/music/music.component').then(m => m.MusicComponent),
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: '/home'
  }
];
