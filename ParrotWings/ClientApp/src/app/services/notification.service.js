var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
var NotificationService = /** @class */ (function () {
    function NotificationService(snackBar, zone) {
        this.snackBar = snackBar;
        this.zone = zone;
    }
    NotificationService.prototype.showSuccess = function (message) {
        var _this = this;
        // Had an issue with the snackbar being ran outside of angular's zone.
        this.zone.run(function () {
            _this.snackBar.open(message);
        });
    };
    NotificationService.prototype.showError = function (message) {
        var _this = this;
        this.zone.run(function () {
            // The second parameter is the text in the button. 
            // In the third, we send in the css class for the snack bar.
            _this.snackBar.open(message, 'X', { panelClass: ['error'] });
        });
    };
    NotificationService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [MatSnackBar,
            NgZone])
    ], NotificationService);
    return NotificationService;
}());
export { NotificationService };
//# sourceMappingURL=notification.service.js.map