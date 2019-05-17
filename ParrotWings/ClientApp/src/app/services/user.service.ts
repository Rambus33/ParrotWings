import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

import { PaymentService } from '../services/payment.service';
import { User } from '../models/user';
import { Amount } from '../models/amount';

@Injectable({ providedIn: 'root' })
export class UserService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  private url = "/api/user";

  constructor(private http: HttpClient, private router: Router, private paymentService: PaymentService,) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  setCurrentUserSubject(user: User) {
    this.currentUserSubject.next(user);
  }

  login(user: User) {
    return this.http.post(this.url + '/login', user);
  }

  registration(user: User) {
    return this.http.post(this.url + '/registration', user);
  }
  
  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.paymentService.amountSubject.next(new Amount);
    this.router.navigate(['/login']);
  }

  getAll() {
    return this.http.get(this.url + '/getAll');
  }
}
