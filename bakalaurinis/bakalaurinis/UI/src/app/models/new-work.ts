import { WorkPriority } from '../components/works-table/works-priority.enum';

export class NewWork {
  title: string;
  description: string;
  userId: number;
  durationInMinutes: number;
  activityPriority: WorkPriority;
  willBeParticipant: boolean;
}
