import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-sign-in-or-register',
  template:
    `<div class="auth-container">
      <div class="auth-box">
        <h2>Sign in or create an account</h2>

        <form [formGroup]="emailForm" (ngSubmit)="onContinue()">
          <label for="email">Enter your email</label>
          <input
            id="email"
            type="email"
            formControlName="email"
            placeholder="example@example.com"
          />
          <div class="error" *ngIf="emailForm.get('email')?.invalid && emailForm.get('email')?.touched">
            Please enter a valid email.
          </div>

          <button type="submit" class="continue-btn">Continue</button>
        </form>

        <p class="terms">
          By continuing, you agree to Amazon's
          <a href="#">Conditions of Use</a> and
          <a href="#">Privacy Notice</a>.
        </p>

        <a class="help-link" href="#">Need help?</a>
      </div>
    </div>`
  ,
  styles: [`
    .auth-container {
      display: flex;
      justify-content: center;
      padding: 3rem;
    }
    .auth-box {
      width: 100%;
      max-width: 350px;
      padding: 2rem;
      border: 1px solid #ddd;
      border-radius: 8px;
      background-color: #fff;
    }
    .auth-box h2 {
      font-size: 20px;
      margin-bottom: 1rem;
    }
    .auth-box label {
      font-weight: 500;
    }
    .auth-box input {
      width: 100%;
      padding: 10px;
      margin-top: 5px;
      margin-bottom: 10px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
    .auth-box .error {
      color: red;
      font-size: 12px;
      margin-bottom: 0.5rem;
    }
    .auth-box .continue-btn {
      width: 100%;
      background-color: #f0c14b;
      border: 1px solid #a88734;
      padding: 10px;
      font-weight: bold;
      cursor: pointer;
    }
    .auth-box .terms {
      font-size: 12px;
      margin-top: 1rem;
    }
    .auth-box .help-link {
      display: block;
      margin-top: 1rem;
      font-size: 14px;
      color: #0066c0;
    }`
  ],
  imports: [CommonModule, ReactiveFormsModule],
})
export class SignInOrRegister implements OnInit {
  emailForm!: FormGroup;

  constructor(private fb: FormBuilder, private router: Router) {}

  ngOnInit(): void {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onContinue() {
    if (this.emailForm.valid) {
      const email = this.emailForm.value.email;
      console.log('Email entered:', email);
      this.router.navigate(['/auth/password'], { queryParams: { email } });
    }
  }
}
