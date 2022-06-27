import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LoginRequest} from './LoginRequest';
import {AuthenticationService} from '../../services/authentication.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    loginRequest: LoginRequest;
    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService) {
        this.createLoginForm();
    }

    ngOnInit(): void {
        this.authenticationService.navigateToDashboardPageIfLoggedIn();
    }

    createLoginForm() {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    login() {

        if (this.loginForm.invalid) {
            return;
        }

        this.loginRequest = Object.assign({}, this.loginForm.value);
        this.authenticationService.login(this.loginRequest);
        this.loginForm.reset();
    }

    invalidEmail() {
        return this.loginForm.get('email').hasError('required') && this.loginForm.get('email').dirty;
    }

    invalidPassword() {
        return this.loginForm.get('password').hasError('required') && this.loginForm.get('password').dirty;
    }

    isDisableLoginButton() {
        return this.loginForm.invalid || this.invalidEmail() || this.invalidPassword();
    }

}
