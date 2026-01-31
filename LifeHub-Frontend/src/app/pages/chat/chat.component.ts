import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="chat-container">
      <h1>Chat en Tiempo Real</h1>
      <p>Funcionalidad de chat próximamente disponible</p>
      <p>Esta sección permitirá chat en tiempo real con tus amigos usando SignalR</p>
    </div>
  `,
  styles: [`
    .chat-container {
      background: white;
      padding: 2rem;
      border-radius: 8px;
      text-align: center;
    }
  `]
})
export class ChatComponent {}
