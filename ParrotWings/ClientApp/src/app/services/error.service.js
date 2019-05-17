var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Injectable } from '@angular/core';
var ErrorService = /** @class */ (function () {
    function ErrorService() {
    }
    ErrorService.prototype.getClientErrorMessage = function (error) {
        return error.message ?
            error.message :
            error.toString();
    };
    ErrorService.prototype.getServerErrorMessage = function (error) {
        return navigator.onLine ?
            error.message :
            'No Internet Connection';
    };
    ErrorService = __decorate([
        Injectable({
            providedIn: 'root'
        })
    ], ErrorService);
    return ErrorService;
}());
export { ErrorService };
//# sourceMappingURL=error.service.js.map