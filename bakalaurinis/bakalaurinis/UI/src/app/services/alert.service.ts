import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private snackBar: MatSnackBar) { }

  public showMessage(message: string): void {
    this.snackBar.open(message, 'Close', { duration: 5000 });
  }

  public showCheckFormMessage(): void {
    this.showMessage('Pasitikrinkite ar užpildėte visus privalomus laukus');
  }
}
