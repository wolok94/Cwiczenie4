import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor() { }

  successMessage : BehaviorSubject<{ message: string; type: string } | null> = new BehaviorSubject<{ message: string; type: string } | null>(null);

  timeoutId: any;

  showMessage(message: string, type: 'success' | 'error') {
    this.successMessage.next({ message, type });
  }
}
