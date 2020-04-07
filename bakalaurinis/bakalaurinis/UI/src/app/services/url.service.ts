import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private readonly url = 'https://localhost:44314/api/';

  getAbsolutePath(extension: string) {
    return this.url + extension;
  }
}
