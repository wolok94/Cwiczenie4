import { Component, OnInit } from "@angular/core";
import { CurrencyAppComponent } from "./currency-app/currency-app.component";
import { MessageComponent } from "./message/message.component";
import { RouterModule, RouterOutlet } from "@angular/router";
import { CommonModule } from "@angular/common";
import { LoadingService } from "./services/loading.service";
import { LoadingModalComponent } from "./loading-modal/loading-modal.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [MessageComponent, RouterModule, CommonModule, LoadingModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'Cwiczenie4_KamilWolak.UI';

  isLoading: boolean = false;

  constructor(private loadingService: LoadingService) {
    
  }
  ngOnInit(): void {
    this.loadingService.isLoading.subscribe(res => {
      this.isLoading = res;
    })
  }

}
