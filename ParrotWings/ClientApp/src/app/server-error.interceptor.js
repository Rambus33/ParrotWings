var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
var ServerErrorInterceptor = /** @class */ (function () {
    function ServerErrorInterceptor() {
    }
    ServerErrorInterceptor.prototype.intercept = function (request, next) {
        return next.handle(request).pipe(retry(1), catchError(function (error) {
            if (error.status === 401) {
                // refresh token
            }
            else {
                return throwError(error);
            }
        }));
    };
    ServerErrorInterceptor = __decorate([
        Injectable()
    ], ServerErrorInterceptor);
    return ServerErrorInterceptor;
}());
export { ServerErrorInterceptor };
//# sourceMappingURL=server-error.interceptor.js.map