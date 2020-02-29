import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ScheduleGenerationService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  generateScheduleForUser(id: number): any {
    return this.http.get<any>(this.urlService.getAbsolutePath('ScheduleGeneration/' + id));
  }
}
