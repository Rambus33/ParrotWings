import { Injectable } from '@angular/core';
import { HttpEvent, HttpRequest, HttpHandler, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { UserService } from './services/user.service';
import { BalanceService } from './services/balance.service';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {
  constructor(private userService: UserService, private balanceService: BalanceService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {       
        if (error.status === 401) {
          this.userService.logout();
        } else {
          return throwError(error);
        }        
      })
    );    
  }
}
