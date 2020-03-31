import { NewActivity } from './new-activity';

export class GetActivities extends NewActivity {
  id: number;
  startTime: Date;
  endTime: Date;
  isConfirmed: boolean;
  isAuthor:boolean;
}
