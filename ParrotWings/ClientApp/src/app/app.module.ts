import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler  } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { GlobalErrorHandler } from './global-error-handler';
import { ServerErrorInterceptor } from './server-error.interceptor';

import { AppComponent } from './app.component';
import { LoginComponent } from './component/login/login.component';
import { PaymentComponent } from './component/payment/payment.component';
import { PaymentActivate } from './guards/paymentActivate';
import { LoginActivate } from './guards/loginActivate';
import { JwtInterceptor } from './jwt.Interceptor';
import { PaymentListComponent } from './component/payment-list/payment-list.component';

const appRoutes: Routes = [
  { path: 'payment', component: PaymentComponent, canActivate: [PaymentActivate] },
  { path: 'login', component: LoginComponent, canActivate: [LoginActivate] },
  { path: '', component: PaymentListComponent, canActivate: [PaymentActivate] },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatSnackBarModule,
    MatSortModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    PaymentComponent,
    PaymentListComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: ServerErrorInterceptor, multi: true }
  ],
    bootstrap: [AppComponent]
})
export class AppModule { }
