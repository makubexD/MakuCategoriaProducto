import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from "@angular/core";
import { environment } from '../environments/environment';
export const routes: Routes = [
  { path: "", redirectTo: "product", pathMatch: 'full'},
 

  { path: 'product', loadChildren: './product/product.module#ProductModule' },
  { path: '**', redirectTo: 'notfound', pathMatch: 'full' }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { enableTracing: !environment.production });
