import { Component } from "@angular/core";
import { CurrencyAppComponent } from "./currency-app/currency-app.component";
import { MessageComponent } from "./message/message.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CurrencyAppComponent, MessageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Cwiczenie4_KamilWolak.UI';
}
