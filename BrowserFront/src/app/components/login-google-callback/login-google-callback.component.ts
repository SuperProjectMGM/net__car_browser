import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login-google-callback',
  standalone: true,
  imports: [],
  templateUrl: './login-google-callback.component.html',
  styleUrl: './login-google-callback.component.css'
})
export class LoginGoogleCallbackComponent {
    constructor(
        private route: ActivatedRoute,
        private authService: AuthService,
        private router: Router
      ) {}
    
      ngOnInit(): void {
        this.route.queryParams.subscribe((params) => {
          const token = params['token'];
          if (token) {
            localStorage.setItem('token', token);
            this.authService.handleGoogleCallback(token);
          } else {
            console.error('Google Login failed: no token received');
            this.router.navigate(['/login']);
          }
        });
      }
  }
