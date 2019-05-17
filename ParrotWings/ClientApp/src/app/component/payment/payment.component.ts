import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '../../models/user';
import { Amount } from '../../models/amount';
import { UserService } from '../../services/user.service';
import { PaymentService } from '../../services/payment.service';
import { BalanceService } from '../../services/balance.service';

@Component({ templateUrl: './payment.component.html' })
export class PaymentComponent implements OnInit{

  users: User[] = new Array();
  transferAmount: Amount = new Amount();

  constructor(
    private userService: UserService,
    private paymentService: PaymentService,
    private balanceService: BalanceService,
    private router: Router) {
    this.paymentService.amountSubject.subscribe((amount: Amount) => {
      this.transferAmount = amount;
    });
  }

  ngOnInit() {
    this.getAllUsers();
  }

  getAllUsers() {
    this.userService.getAll()
      .subscribe((data: User[]) => {
        this.users = data;
      });
  }

  transfer(amount: Amount) {
    this.paymentService.transfer(amount)
      .subscribe(() => {
        this.balanceService.getBalance();
        this.router.navigate(['/']);
      });
  }
}
