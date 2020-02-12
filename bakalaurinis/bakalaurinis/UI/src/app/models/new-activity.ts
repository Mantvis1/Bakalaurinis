import { ActivityPriority } from '../components/activities-table/activity-priority.enum';

export class NewActivity {
  Title: string;
  Description: string;
  UserId: number;
  StartDate: Date
  EndDate: Date
  FinishUntil: Date
  ActivityPriority: ActivityPriority
}
