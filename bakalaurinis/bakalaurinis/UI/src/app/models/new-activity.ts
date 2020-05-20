import { ActivityPriority } from '../components/works-table/works-priority.enum';

export class NewActivity {
  title: string;
  description: string;
  userId: number;
  durationInMinutes: number;
  activityPriority: ActivityPriority;
  willBeParticipant: boolean;
}
