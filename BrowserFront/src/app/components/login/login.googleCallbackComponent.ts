import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-google-callback',
  template: `<p>Processing Google Login...</p>`,
})
export class GoogleCallbackComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const token = params['token'];
      if (token) {
        this.authService.handleGoogleCallback(token);
      } else {
        console.error('Google Login failed: no token received');
        this.router.navigate(['/login']);
      }
    });
  }
}