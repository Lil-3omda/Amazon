import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignInOrRegister } from './sign-in-or-register';

describe('SignInOrRegister', () => {
  let component: SignInOrRegister;
  let fixture: ComponentFixture<SignInOrRegister>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignInOrRegister]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignInOrRegister);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
