import { Routes } from '@angular/router';
import { SignInOrRegister } from '../pages/sign-in-or-register/sign-in-or-register';
import { SignInPassword } from '../pages/sign-in-password/sign-in-password';

export const authRoutes: Routes = [
  { path: 'email', component: SignInOrRegister },
  { path: 'password', component: SignInPassword }
];
