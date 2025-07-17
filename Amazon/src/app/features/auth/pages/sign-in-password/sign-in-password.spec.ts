import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignInPassword } from './sign-in-password';

describe('SignInPassword', () => {
  let component: SignInPassword;
  let fixture: ComponentFixture<SignInPassword>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignInPassword]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignInPassword);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
