import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CurrencyAppComponent } from "./currency-app/currency-app.component";

@Component({
  selector: 'app-root',
  imports: [ CurrencyAppComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Cwiczenie4_KamilWolak.UI';
}
