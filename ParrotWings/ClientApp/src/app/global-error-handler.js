var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggingService } from './services/logging.service';
import { ErrorService } from './services/error.service';
import { NotificationService } from './services/notification.service';
var GlobalErrorHandler = /** @class */ (function () {
    function GlobalErrorHandler(injector) {
        this.injector = injector;
    }
    GlobalErrorHandler.prototype.handleError = function (error) {
        var errorService = this.injector.get(ErrorService);
        var logger = this.injector.get(LoggingService);
        var notifier = this.injector.get(NotificationService);
        var message;
        var stackTrace;
        if (error instanceof HttpErrorResponse) {
            // Server error
            message = errorService.getServerErrorMessage(error);
            //stackTrace = errorService.getServerErrorStackTrace(error);
            notifier.showError(message);
        }
        else {
            // Client Error
            message = errorService.getClientErrorMessage(error);
            notifier.showError(message);
        }
        // Always log errors
        logger.logError(message, stackTrace);
        console.error(error);
    };
    GlobalErrorHandler = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Injector])
    ], GlobalErrorHandler);
    return GlobalErrorHandler;
}());
export { GlobalErrorHandler };
//# sourceMappingURL=global-error-handler.js.map