import { ActivityPriority } from '../components/activities-table/activity-priority.enum';

export class GetActivities {
  Id: number;
  Title: string;
  Description: string;
  UserId: number;
  StartDate: Date
  EndDate: Date
  FinishUntil: Date
  ActivityPriority: ActivityPriority
}
