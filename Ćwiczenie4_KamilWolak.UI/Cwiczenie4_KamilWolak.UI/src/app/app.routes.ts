import { Routes } from "@angular/router";
import { CurrencyAppComponent } from "./currency-app/currency-app.component";
import { LoadDbModalComponent } from "./load-db-modal/load-db-modal.component";


export const routes: Routes = [
    {
        path: "", component: CurrencyAppComponent, children: [
            {path: "loadDatabase", component: LoadDbModalComponent}
    ]},
    
];
