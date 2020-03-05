import { ActivityPriority } from '../components/activities-table/activity-priority.enum';

export class NewActivity {
  title: string;
  description: string;
  userId: number;
  durationInMinutes: number;
  activityPriority: ActivityPriority;
}
