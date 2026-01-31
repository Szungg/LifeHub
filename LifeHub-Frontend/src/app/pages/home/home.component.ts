import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="home-container">
      <h1>Bienvenido a LifeHub</h1>
      <p>Tu portal personal para herramientas útiles del día a día</p>
      
      <div class="features-grid">
        <div class="feature-card">
          <h3>👥 Amigos</h3>
          <p>Gestiona tu lista de amigos</p>
          <a routerLink="/friends" class="btn-primary">Ver Amigos</a>
        </div>
        
        <div class="feature-card">
          <h3>💬 Chat</h3>
          <p>Comunícate en tiempo real</p>
          <a routerLink="/chat" class="btn-primary">Abrir Chat</a>
        </div>
        
        <div class="feature-card">
          <h3>📚 Recomendaciones</h3>
          <p>Recomienda películas y libros</p>
          <a routerLink="/recommendations" class="btn-primary">Ver Recomendaciones</a>
        </div>
        
        <div class="feature-card">
          <h3>📄 Documentos</h3>
          <p>Crea y edita documentos</p>
          <a routerLink="/documents" class="btn-primary">Mis Documentos</a>
        </div>
        
        <div class="feature-card">
          <h3>🎵 Música</h3>
          <p>Reproductor de música local</p>
          <a routerLink="/music" class="btn-primary">Mi Música</a>
        </div>
        
        <div class="feature-card">
          <h3>👤 Perfil</h3>
          <p>Actualiza tu perfil</p>
          <a routerLink="/profile" class="btn-primary">Mi Perfil</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .home-container {
      text-align: center;
    }
    
    h1 {
      color: #2c3e50;
      margin-bottom: 1rem;
    }
    
    .features-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 2rem;
      margin-top: 3rem;
    }
    
    .feature-card {
      background: white;
      padding: 2rem;
      border-radius: 8px;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }
    
    .feature-card h3 {
      margin-bottom: 0.5rem;
      color: #2c3e50;
    }
    
    .feature-card a {
      display: inline-block;
      margin-top: 1rem;
      text-decoration: none;
      padding: 0.5rem 1rem;
      background-color: #3498db;
      color: white;
      border-radius: 4px;
    }
    
    .feature-card a:hover {
      background-color: #2980b9;
    }
  `]
})
export class HomeComponent {}
