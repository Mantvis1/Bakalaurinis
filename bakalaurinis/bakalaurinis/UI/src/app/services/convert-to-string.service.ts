import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { InvitationStatus } from '../models/invitation-status.enum';
import { WorkPriority } from '../components/works-table/works-priority.enum';

@Injectable({
  providedIn: 'root'
})
export class ConvertToStringService {

  constructor(
    private datePipe: DatePipe
  ) { }

  getHoursAndMinutesFromDate(date: Date): string {
    return this.datePipe.transform(date, 'HH:mm');
  }
  getFullDate(currentDate: Date): string {
    return this.datePipe.transform(currentDate, 'yyyy-MM-dd');
  }

  getInvitationStatusByIndex(index: number): string {
    return InvitationStatus[index];
  }

  getFullDateAndTime(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd HH:mm:ss');
  }

  getWorkPriorityByIndex(index: number) {
    return WorkPriority[index];
  }

}
