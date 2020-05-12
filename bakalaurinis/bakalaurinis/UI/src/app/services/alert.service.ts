import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private snackBar: MatSnackBar) { }

  public showMessage(message: string): void {
    this.snackBar.open(message, 'X', { duration: 5000 });
  }

  public showCheckFormMessage(): void {
    this.showMessage('Some of required form fields are empty');
  }
}
