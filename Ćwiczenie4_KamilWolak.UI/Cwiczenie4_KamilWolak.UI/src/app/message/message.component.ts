import { Component } from '@angular/core';
import { MessageService } from '../services/message.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent {
  message: string = '';
  timeoutId: any;
  type: string = '';
  constructor(private messageService : MessageService) {

  }
  ngOnInit(): void {
    this.messageService.successMessage.subscribe(res => {
      if (this.timeoutId) {
        clearTimeout(this.timeoutId);
      }
      this.message = res != undefined ? res?.message : '';
      this.type = res != undefined ? res?.type : '';
      this.timeoutId = setTimeout(() => {
        this.message = '';
      }, 3000);
    })

  }

}
