import { Component } from '@angular/core';

import { UserService } from './services/user.service';
import { BalanceService } from './services/balance.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public currentUser: User;
  public currentBalance: number;
  constructor(private userService: UserService, private balanceService: BalanceService) {
    this.userService.currentUser.subscribe(x => {
      this.currentUser = x;
      if (this.currentUser) {
        this.balanceService.getBalance();
      }
    });
    this.balanceService.currentBalance.subscribe(x => { this.currentBalance = x });
  }

  logout() {
    this.userService.logout();
  }
}
