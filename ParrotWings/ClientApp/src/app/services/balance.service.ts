import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BalanceService {
  private currentBalanceSubject: BehaviorSubject<number>;
  public currentBalance: Observable<number>;
  private url = "/api/balance";

  constructor(private http: HttpClient) {
    this.currentBalanceSubject = new BehaviorSubject<number>(0);
    this.currentBalance = this.currentBalanceSubject.asObservable();
  }

  setCurrentBalanceSubject(balance: number) {
    this.currentBalanceSubject.next(balance);
  }
  getBalance() {
    return this.http.get(this.url)
      .subscribe((data: number) => {
        this.setCurrentBalanceSubject(data)
      });
  }
}
