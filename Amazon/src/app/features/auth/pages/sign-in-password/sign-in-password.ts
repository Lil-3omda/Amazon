import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-sign-in-password',
  template: `
    <div class="auth-container">
      <div class="auth-box">
        <h2>Sign in</h2>
        <p class="email-line">
          {{ email }} <a (click)="goBack()" class="change-link">Change</a>
        </p>

        <form [formGroup]="passwordForm" (ngSubmit)="onSubmit()">
          <label for="password">Password</label>
          <input
            id="password"
            type="password"
            formControlName="password"
            placeholder="Enter your password"
          />
          <div class="error" *ngIf="passwordForm.get('password')?.invalid && passwordForm.get('password')?.touched">
            Password is required.
          </div>

          <a class="forgot-link" href="#">Forgot password?</a>

          <button type="submit" class="sign-in-btn">Sign in</button>
        </form>
      </div>
    </div>
  `,
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
    .auth-box .sign-in-btn {
      width: 100%;
      background-color: #f0c14b;
      border: 1px solid #a88734;
      padding: 10px;
      font-weight: bold;
      cursor: pointer;
    }
    .auth-box .email-line {
      font-size: 14px;
      margin-bottom: 1rem;
    }
    .auth-box .change-link {
      margin-left: 1rem;
      color: #007185;
      cursor: pointer;
    }
    .auth-box .forgot-link {
      display: block;
      margin-bottom: 1rem;
      font-size: 13px;
      color: #007185;
    }
  `],
  imports: [CommonModule, ReactiveFormsModule],
})
export class SignInPassword implements OnInit {
  passwordForm!: FormGroup;
  email!: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'] || '';
    });

    this.passwordForm = this.fb.group({
      password: ['', Validators.required]
    });
  }

  goBack() {
    this.router.navigate(['/auth/email']);
  }

  onSubmit() {
    if (this.passwordForm.valid) {
      const credentials = {
        email: this.email,
        password: this.passwordForm.value.password
      };

      console.log('Logging in with:', credentials);
      // TODO: Call AuthService.login(credentials)
    }
  }
}
