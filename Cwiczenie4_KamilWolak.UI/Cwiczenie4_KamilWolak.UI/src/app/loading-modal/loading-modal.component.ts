import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { LoadingService } from '../services/loading.service';
declare var bootstrap: any;
@Component({
  selector: 'app-loading-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading-modal.component.html',
  styleUrl: './loading-modal.component.css'
})
export class LoadingModalComponent implements OnInit{

  isLoading: boolean = false;
  ngOnInit(): void {

    const modalElement = document.getElementById('loadingModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show(); 
    }

    this.loadingService.isLoading.subscribe(res => {
      this.isLoading = res;
    })
  }

  constructor(private loadingService: LoadingService) {
    
  }


  

}
