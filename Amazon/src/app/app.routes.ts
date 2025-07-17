import { Routes } from '@angular/router';
import { ProductCard } from './layout/product-card/product-card';
import { SignInOrRegister } from './features/auth/pages/sign-in-or-register/sign-in-or-register';
import { App } from './app';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // redirect root to /home
  { path: 'home', component: App }, // render the actual home page
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/services/auth.routes').then((m) => m.authRoutes),
  },
];
