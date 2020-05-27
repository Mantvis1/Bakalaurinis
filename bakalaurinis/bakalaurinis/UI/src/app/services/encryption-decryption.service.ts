import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptionDecryptionService {

  encrypt(input: string) {
    return CryptoJS.SHA256(input.trim()).toString();
  }
}
