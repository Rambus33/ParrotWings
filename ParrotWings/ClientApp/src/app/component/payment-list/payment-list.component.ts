import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Sort, MatSort } from '@angular/material';

import { Amount } from '../../models/amount';
import { Payment } from '../../models/payment';
import { PaymentService } from '../../services/payment.service';

@Component({ templateUrl: './payment-list.component.html' })
export class PaymentListComponent implements OnInit {

  payments: Payment[] = new Array();
  sort: MatSort;

  constructor(private paymentService: PaymentService, private router: Router) { }

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.paymentService.getAll()
      .subscribe((data: Payment[]) => {
        this.payments = data;
      });
  }

  RowSelected(payment: Payment) {
    this.paymentService.amountSubject.next(new Amount(payment.correspondentUser.userId, Math.abs(payment.amount)));
    this.router.navigate(['/payment']);
  }

  sortData(sort: Sort) {
    if (!sort.active || sort.direction === '') {
      return;
    }
    this.payments = this.payments.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'Amount': return compare(a.amount, b.amount, isAsc);
        case 'Name': return compare(a.correspondentUser.name, b.correspondentUser.name, isAsc);
        case 'Balance': return compare(a.balance, b.balance, isAsc);
        case 'Date': return compare(a.date, b.date, isAsc);
        default: return 0;
      }
    });
  }
}

function compare(a: number | string | Date, b: number | string| Date, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
