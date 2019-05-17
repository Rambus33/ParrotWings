import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { Amount } from '../models/amount'

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private url = "/api/payment";
  amountSubject: BehaviorSubject<Amount> = new BehaviorSubject(new Amount());

  constructor(private http: HttpClient) { }

  transfer(amount: Amount) {
    return this.http.post(this.url, amount);
  }

  getAll() {
    return this.http.get(this.url + '/getAll');
  }
}
