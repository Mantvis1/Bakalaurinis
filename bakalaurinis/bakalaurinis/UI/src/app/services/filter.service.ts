import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  constructor() { }

  getFilteredValue(value: string) {
    return value.trim().toLowerCase();
  }
}
