import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

import { User } from '../../models/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'})
export class LoginComponent implements OnInit {
  loginUser: User = new User();
  registerUser: User = new User();
  passwordConfirm: string = "";

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit() { }

  login(u: User) {
    this.userService.login(u)
      .subscribe((data: User) => {
        localStorage.setItem('currentUser', JSON.stringify(data));
        this.userService.setCurrentUserSubject(data);
        this.router.navigate(['']);
      });
  }

  registration(u: User, passwordConfirm: string) {
    if (u.password == passwordConfirm) {
      this.userService.registration(u)
        .pipe(tap(() => this.login(u))).subscribe();
    } else {
      throw new Error("Passwords don't match");
    }
  }  
}
